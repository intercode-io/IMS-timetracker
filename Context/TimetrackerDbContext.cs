using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IMS_Timetracker.Entities;
using IMS_Timetracker.Entities.Privileges;
using IMS_Timetracker.Enums;

namespace IMS_Timetracker.Context
{
    public class TimetrackerDbContext : DbContext
    {
        public TimetrackerDbContext(DbContextOptions<TimetrackerDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ProjectUserRole> ProjectsUsersRoles { get; set; }
        public DbSet<RolePermission> RolesPermissions { get; set; }
        public DbSet<TimeLog> TimeLogs { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<UserDetail>().HasKey(ud => ud.Id);
            modelBuilder.Entity<Project>().HasKey(p => p.Id);
            modelBuilder.Entity<Role>().HasKey(r => r.Id);
            modelBuilder.Entity<ProjectUserRole>().HasKey(r => r.UserId);
            modelBuilder.Entity<RolePermission>().HasKey(r => r.Id);
            modelBuilder.Entity<TimeLog>().HasKey(r => r.Id);
            
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<UserDetail>().ToTable("UserDetails");
            modelBuilder.Entity<Project>().ToTable("Projects");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<ProjectUserRole>().ToTable("ProjectsUsersRoles");
            modelBuilder.Entity<RolePermission>().ToTable("RolesPermissions");
            modelBuilder.Entity<TimeLog>().ToTable("TimeLogs");
            
            modelBuilder.Entity<Project>()
                .HasMany(a => a.TimeLogs)
                .WithOne(at => at.Project)
                .HasForeignKey(at => at.ProjectId); 
            
            modelBuilder.Entity<User>()
                .HasMany(a => a.TimeLogs)
                .WithOne(at => at.User)
                .HasForeignKey(at => at.UserId); 
            
            modelBuilder.Entity<Project>()
                .HasMany(a => a.ProjectsUsersRoles)
                .WithOne(at => at.Project)
                .HasForeignKey(at => at.ProjectId);
            
            modelBuilder.Entity<User>()
                .HasMany(t => t.ProjectsUsersRoles)
                .WithOne(at => at.User)
                .HasForeignKey(t => t.UserId);
            
            modelBuilder.Entity<Role>()
                .HasMany(t => t.ProjectsUsersRoles)
                .WithOne(at => at.Role)
                .HasForeignKey(t => t.RoleId);
            
            modelBuilder.Entity<Role>()
                .HasMany(t => t.RolesPermissions)
                .WithOne(at => at.Role)
                .HasForeignKey(t => t.RoleId);
            
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
