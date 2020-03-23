using System.Collections.Generic;

namespace Timetracker.Entities.Data
{
    public class ProjectEntity
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        public ICollection<ProjectUserRoleEntity> ProjectsUsersRoles { get; set; }

        public string Color { get; set; }
    }
}
