using System.Collections.Generic;
using IMS_Timetracker.Entities;

namespace IMS_Timetracker.Entities.Privileges
{
    public class ProjectUserRole
    {
        
        public int Id { get; set; }
        
        public int? ProjectId { get; set; }
        public ProjectEntity ProjectEntity { get; set; }
        
        public int UserId { get; set; }
        public UserEntity UserEntity { get; set; }
        
        public int RoleId { get; set; }
        public Role Role { get; set; }
        
        public ICollection<TimeLogEntity> TimeLogs { get; set; }


    }
}