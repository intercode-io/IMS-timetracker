using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IMS_Timetracker.Context;
using Microsoft.EntityFrameworkCore;
using System.IO;
using IMS_Timetracker.Dto;
using IMS_Timetracker.Abstraction;
using IMS_Timetracker.Mappers;
using IMS_Timetracker.Services;

namespace IMS_Timetracker
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
            string connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IMapper<Entities.UserEntity, User>, UserMapper>();
            services.AddSingleton<IMapper<Entities.ProjectEntity, Project>, ProjectMapper>();
            services.AddSingleton<IMapper<Entities.TimeLogEntity, TimeLog>, TiemLogMapper>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IUserServivce, UserServivce>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITimeLogService, TimeLogService>();

            services.AddDbContext<TimetrackerDbContext>(x => x.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // global cors policy
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

            app.UseMvc();
        }
    }
}
