﻿// <auto-generated />
using System;
using IMS_Timetracker.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IMS_Timetracker.Migrations
{
    [DbContext(typeof(TimetrackerDbContext))]
    [Migration("20190929154301_AddDefaultProjects")]
    partial class AddDefaultProjects
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IMS_Timetracker.Entities.Privileges.ProjectUserRole", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProjectId");

                    b.Property<int?>("RoleId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectsUsersRoles");
                });

            modelBuilder.Entity("IMS_Timetracker.Entities.Privileges.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Manager"
                        },
                        new
                        {
                            Id = 2,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("IMS_Timetracker.Entities.Privileges.RolePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Permission");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RolesPermissions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Permission = 16,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 2,
                            Permission = 17,
                            RoleId = 2
                        },
                        new
                        {
                            Id = 3,
                            Permission = 22,
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("IMS_Timetracker.Entities.Project", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "Project 1"
                        },
                        new
                        {
                            Id = 2,
                            Title = "Project 2"
                        },
                        new
                        {
                            Id = 3,
                            Title = "Project 3"
                        });
                });

            modelBuilder.Entity("IMS_Timetracker.Entities.TimeLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<float>("Hours");

                    b.Property<int>("ProjectId");

                    b.Property<byte>("TimeEnd")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<byte>("TimeStart")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("TimeLogs");
                });

            modelBuilder.Entity("IMS_Timetracker.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Vialik"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Alex"
                        });
                });

            modelBuilder.Entity("IMS_Timetracker.Entities.UserDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserDetails");
                });

            modelBuilder.Entity("IMS_Timetracker.Entities.Privileges.ProjectUserRole", b =>
                {
                    b.HasOne("IMS_Timetracker.Entities.Project", "Project")
                        .WithMany("ProjectsUsersRoles")
                        .HasForeignKey("ProjectId");

                    b.HasOne("IMS_Timetracker.Entities.Privileges.Role", "Role")
                        .WithMany("ProjectsUsersRoles")
                        .HasForeignKey("RoleId");

                    b.HasOne("IMS_Timetracker.Entities.User", "User")
                        .WithMany("ProjectsUsersRoles")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("IMS_Timetracker.Entities.Privileges.RolePermission", b =>
                {
                    b.HasOne("IMS_Timetracker.Entities.Privileges.Role", "Role")
                        .WithMany("RolesPermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IMS_Timetracker.Entities.TimeLog", b =>
                {
                    b.HasOne("IMS_Timetracker.Entities.Project", "Project")
                        .WithMany("TimeLogs")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("IMS_Timetracker.Entities.User", "User")
                        .WithMany("TimeLogs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IMS_Timetracker.Entities.UserDetail", b =>
                {
                    b.HasOne("IMS_Timetracker.Entities.User", "User")
                        .WithOne("UserDetail")
                        .HasForeignKey("IMS_Timetracker.Entities.UserDetail", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
