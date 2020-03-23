using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Timetracker.DAL.Configurations;
using Timetracker.Entities.Constants;
using Timetracker.Entities.Data;

namespace Timetracker.DAL.Context
{
    public class TimetrackerDbContext : IdentityDbContext<UserEntity, RoleEntity, int>
    {
        public TimetrackerDbContext(DbContextOptions<TimetrackerDbContext> options) : base(options) { }

        public DbSet<ProjectEntity> Projects { get; set; }

        public DbSet<RolePermissionEntity> RolesPermissions { get; set; }

        public DbSet<TimeLogEntity> TimeLogs { get; set; }

        public DbSet<UserProjectsEntity> UserProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProjectEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TimeLogEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserProjectsEntityConfiguration());

            ConfigureIdentityTables(modelBuilder);

            SeedDatabase(modelBuilder);
        }

        private void ConfigureIdentityTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserClaim<int>>(b =>
            {
                b.ToTable("UserClaims");
            });

            modelBuilder.Entity<IdentityUserToken<int>>(b =>
            {
                b.ToTable("UserTokens");
            });

            modelBuilder.Entity<IdentityRoleClaim<int>>(b =>
            {
                b.ToTable("RoleClaims");
            });

            modelBuilder.Entity<IdentityUserRole<int>>(b =>
            {
                b.ToTable("UserRoles");
            });

            modelBuilder.Entity<IdentityUserLogin<int>>(b =>
            {
                b.ToTable("UserLogins");
            });
        }

        void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleEntity>().HasData(
                new RoleEntity
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new RoleEntity
                {
                    Id = 2,
                    Name = "User",
                    NormalizedName = "USER"
                }
            );

            modelBuilder.Entity<RolePermissionEntity>().HasData(
                new RolePermissionEntity
                {
                    Id = 1,
                    RoleId = 1,
                    Permission = Permissions.ProjectAll,
                },
                new RolePermissionEntity
                {
                    Id = 2,
                    RoleId = 2,
                    Permission = Permissions.ProjectRead,
                },
                new RolePermissionEntity
                {
                    Id = 3,
                    RoleId = 2,
                    Permission = Permissions.ProjectLogTime,
                }
            );
        }
    }
}
