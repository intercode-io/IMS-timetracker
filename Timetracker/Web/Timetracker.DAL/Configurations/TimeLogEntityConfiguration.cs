using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timetracker.Entities.Data;

namespace Timetracker.DAL.Configurations
{
    public class TimeLogEntityConfiguration : IEntityTypeConfiguration<TimeLogEntity>
    {
        public void Configure(EntityTypeBuilder<TimeLogEntity> builder)
        {
            builder.ToTable("TimeLogs");

            builder.HasKey(r => r.Id);
        }
    }
}
