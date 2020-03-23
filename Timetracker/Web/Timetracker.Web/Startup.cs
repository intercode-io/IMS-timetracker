using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Timetracker.BLL.Mappers.Implementations;
using Timetracker.BLL.Services.Interfaces;
using Timetracker.BLL.Services.Implementations;
using Timetracker.DAL.Context;
using Timetracker.Entities.Data;
using Timetracker.Models.Data;
using Timetracker.BLL.Mappers.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Timetracker.BLL.Configuration;

namespace Timetracker.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(
            IConfiguration configuration,
            IHostingEnvironment env
            )
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TimetrackerDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<UserEntity, RoleEntity>()
                .AddEntityFrameworkStores<TimetrackerDbContext>();

            services.AddCors();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(jwtOptions =>
                    {
                        jwtOptions.RequireHttpsMetadata = false;
                        jwtOptions.SaveToken = true;
                        jwtOptions.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("JwtSettings")["Secret"])),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });

            services.AddSingleton<IMapper<UserEntity, UserModel>, UserMapper>();
            services.AddSingleton<IMapper<ProjectEntity, ProjectModel>, ProjectMapper>();
            services.AddSingleton<IMapper<TimeLogEntity, TimeLogModel>, TimeLogMapper>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITimeLogService, TimeLogService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder =>
                builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed((host) => true)
                    .WithOrigins(new string[]
                    {
                        "http://localhost:4200",
                    })
                    .AllowCredentials()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 &&
                   !Path.HasExtension(context.Request.Path.Value) &&
                   !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });
            app.UseMvcWithDefaultRoute();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
