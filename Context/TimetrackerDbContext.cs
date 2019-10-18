using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMS_Timetracker.Dto.Privileges;
using Microsoft.EntityFrameworkCore;
using IMS_Timetracker.Entities;
using IMS_Timetracker.Entities.Privileges;
using IMS_Timetracker.Enums;

namespace IMS_Timetracker.Context
{
    public class TimetrackerDbContext : DbContext
    {
        public TimetrackerDbContext(DbContextOptions<TimetrackerDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserDetailEntity> UserDetails { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ProjectUserRole> ProjectsUsersRoles { get; set; }
        public DbSet<RolePermission> RolesPermissions { get; set; }
        public DbSet<TimeLogEntity> TimeLogs { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasKey(u => u.Id);
            modelBuilder.Entity<UserDetailEntity>().HasKey(ud => ud.Id);
            modelBuilder.Entity<ProjectEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<Role>().HasKey(r => r.Id);
            modelBuilder.Entity<ProjectUserRole>().HasKey(r => r.Id);
            modelBuilder.Entity<RolePermission>().HasKey(r => r.Id);
            modelBuilder.Entity<TimeLogEntity>().HasKey(r => r.Id);
            
            modelBuilder.Entity<UserEntity>().ToTable("Users");
            modelBuilder.Entity<UserDetailEntity>().ToTable("UserDetails");
            modelBuilder.Entity<ProjectEntity>().ToTable("Projects");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<ProjectUserRole>().ToTable("ProjectsUsersRoles");
            modelBuilder.Entity<RolePermission>().ToTable("RolesPermissions");
            modelBuilder.Entity<TimeLogEntity>().ToTable("TimeLogs");
            
//            modelBuilder.Entity<ProjectEntity>()
//                .HasMany(a => a.TimeLogs)
//                .WithOne(at => at.ProjectEntity)
//                .HasForeignKey(at => at.ProjectId); 
//            
//            modelBuilder.Entity<UserEntity>()
//                .HasMany(a => a.TimeLogs)
//                .WithOne(at => at.UserEntity)
//                .HasForeignKey(at => at.UserId); 
            
            modelBuilder.Entity<ProjectEntity>()
                .HasMany(a => a.ProjectsUsersRoles)
                .WithOne(at => at.ProjectEntity)
                .HasForeignKey(at => at.ProjectId);
            
            modelBuilder.Entity<UserEntity>()
                .HasMany(t => t.ProjectsUsersRoles)
                .WithOne(at => at.UserEntity)
                .HasForeignKey(t => t.UserId);
            
            modelBuilder.Entity<Role>()
                .HasMany(t => t.ProjectsUsersRoles)
                .WithOne(at => at.Role)
                .HasForeignKey(t => t.RoleId);
            
            modelBuilder.Entity<Role>()
                .HasMany(t => t.RolesPermissions)
                .WithOne(at => at.Role)
                .HasForeignKey(t => t.RoleId);

            modelBuilder.Entity<ProjectUserRole>()
                .HasMany(t => t.TimeLogs)
                .WithOne(at => at.ProjectUserRole)
                .HasForeignKey(t => t.ProjectUserRoleId);
            
            SeedDatabase(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        
        void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role {
                    Id = 1,
                    Name = "Manager",
                },
                new Role {
                    Id = 2,
                    Name = "User",
                }
            );
            
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity {
                    Id = 1,
                    FirstName = "Vialik"
                }
            );            
            
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity {
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

            
            modelBuilder.Entity<ProjectUserRole>().HasData(
                new ProjectUserRole
                {
                    UserId = 2,
                    ProjectId = 1,
                    RoleId = 1,
                    Id = 6
                },
                new ProjectUserRole
                {
                    UserId = 2,
                    ProjectId = 2,
                    RoleId = 1,
                    Id = 7
                },
                new ProjectUserRole
                {
                    UserId = 2,
                    ProjectId = 3,
                    RoleId = 1,
                    Id = 8
                });            
            

            
            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission {
                    Id = 1,
                    RoleId = 1,
                    Permission = Permissions.ProjectAll,
                },
                new RolePermission {
                    Id = 2,
                    RoleId = 2,
                    Permission = Permissions.ProjectRead,
                },
                new RolePermission {
                    Id = 3,
                    RoleId = 2,
                    Permission = Permissions.ProjectLogTime,
                }
            );
        }
    }
}
