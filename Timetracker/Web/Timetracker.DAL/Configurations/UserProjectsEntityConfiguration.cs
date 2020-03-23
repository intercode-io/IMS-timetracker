using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timetracker.Entities.Data;

namespace Timetracker.DAL.Configurations
{
    public class UserProjectsEntityConfiguration : IEntityTypeConfiguration<UserProjectsEntity>
    {
        public void Configure(EntityTypeBuilder<UserProjectsEntity> builder)
        {
            builder.HasKey(up => new { up.UserId, up.ProjectId });

            builder.HasOne(up => up.Project)
                .WithMany(p => p.UserProjects)
                .HasForeignKey(up => up.ProjectId);

            builder.HasOne(up => up.User)
                .WithMany(u => u.UserProjects)
                .HasForeignKey(up => up.UserId);
        }
    }
}
