using System.Collections.Generic;
using IMS_Timetracker.Entities.Privileges;

namespace IMS_Timetracker.Dto
{
    public class Project
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        public string Color { get; set; }

        public ICollection<ProjectUserRole> ProjectsUsersRoles { get; set; }
    }
}