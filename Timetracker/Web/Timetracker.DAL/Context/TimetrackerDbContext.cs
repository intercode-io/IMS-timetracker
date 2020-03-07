using Microsoft.EntityFrameworkCore;
using Timetracker.DAL.Configurations;
using Timetracker.Entities.Constants;
using Timetracker.Entities.Data;

namespace Timetracker.DAL.Context
{
    public class TimetrackerDbContext : DbContext
    {
        public TimetrackerDbContext(DbContextOptions<TimetrackerDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<UserDetailEntity> UserDetails { get; set; }

        public DbSet<ProjectEntity> Projects { get; set; }

        public DbSet<RoleEntity> Roles { get; set; }

        public DbSet<ProjectUserRoleEntity> ProjectsUsersRoles { get; set; }

        public DbSet<RolePermissionEntity> RolesPermissions { get; set; }

        public DbSet<TimeLogEntity> TimeLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProjectEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectUserRoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TimeLogEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserDetailEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());

            SeedDatabase(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleEntity>().HasData(
                new RoleEntity
                {
                    Id = 1,
                    Name = "Manager",
                },
                new RoleEntity
                {
                    Id = 2,
                    Name = "User",
                }
            );

            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = 1,
                    FirstName = "Vialik"
                }
            );

            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = 2,
                    FirstName = "Alex"
                }
            );


            modelBuilder.Entity<ProjectEntity>().HasData(
                new ProjectEntity
                {
                    Id = 1,
                    Title = "Project 1"
                }
            );

            modelBuilder.Entity<ProjectEntity>().HasData(
                new ProjectEntity
                {
                    Id = 2,
                    Title = "Project 2"
                }
            );

            modelBuilder.Entity<ProjectEntity>().HasData(
                new ProjectEntity
                {
                    Id = 3,
                    Title = "Project 3"
                }
            );


            modelBuilder.Entity<ProjectUserRoleEntity>().HasData(
                new ProjectUserRoleEntity
                {
                    UserId = 2,
                    ProjectId = 1,
                    RoleId = 1,
                    Id = 6
                },
                new ProjectUserRoleEntity
                {
                    UserId = 2,
                    ProjectId = 2,
                    RoleId = 1,
                    Id = 7
                },
                new ProjectUserRoleEntity
                {
                    UserId = 2,
                    ProjectId = 3,
                    RoleId = 1,
                    Id = 8
                });



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
