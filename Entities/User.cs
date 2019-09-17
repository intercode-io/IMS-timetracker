using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IMS_Timetracker.Entities.Privileges;

namespace IMS_Timetracker.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public UserDetail UserDetail { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
        
        public ICollection<ProjectUserRole> ProjectsUsersRoles { get; set; }
        
        public ICollection<TimeLog> TimeLogs { get; set; }
    }
}