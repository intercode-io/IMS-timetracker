using System.Collections.Generic;
using IMS_Timetracker.Entities.Privileges;

namespace IMS_Timetracker.Entities
{
    public class Project
    {
        public int? Id { get; set; }
        
        public string Title { get; set; }
        public ICollection<ProjectUserRole> ProjectsUsersRoles { get; set; }
        public ICollection<TimeLog> TimeLogs { get; set; }
    }
}