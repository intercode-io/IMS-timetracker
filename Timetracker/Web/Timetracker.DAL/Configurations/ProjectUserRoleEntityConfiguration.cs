using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timetracker.Entities.Data;

namespace Timetracker.DAL.Configurations
{
    public class ProjectUserRoleEntityConfiguration : IEntityTypeConfiguration<ProjectUserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<ProjectUserRoleEntity> builder)
        {
            builder.ToTable("ProjectsUsersRoles");

            builder.HasKey(r => r.Id);
            builder.HasMany(t => t.TimeLogs)
                .WithOne(at => at.ProjectUserRole)
                .HasForeignKey(t => t.ProjectUserRoleId);
        }
    }
}
