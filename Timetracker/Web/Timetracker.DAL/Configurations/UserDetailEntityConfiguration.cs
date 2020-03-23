using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timetracker.Entities.Data;

namespace Timetracker.DAL.Configurations
{
    public class UserDetailEntityConfiguration : IEntityTypeConfiguration<UserDetailEntity>
    {
        public void Configure(EntityTypeBuilder<UserDetailEntity> builder)
        {
            builder.ToTable("UserDetails");

            builder.HasKey(ud => ud.Id);
        }
    }
}
