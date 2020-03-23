using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timetracker.Entities.Data;

namespace Timetracker.DAL.Configurations
{
    public class ProjectEntityConfiguration : IEntityTypeConfiguration<ProjectEntity>
    {
        public void Configure(EntityTypeBuilder<ProjectEntity> builder)
        {
            builder.ToTable("Projects");

            builder.HasKey(p => p.Id);
            builder.HasMany(a => a.UserProjects)
                .WithOne(at => at.Project)
                .HasForeignKey(at => at.ProjectId);
        }
    }
}
