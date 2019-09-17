using IMS_Timetracker.Entities;

namespace IMS_Timetracker.Entities.Privileges
{
    public class ProjectUserRole
    {
        public int ProjectId { get; set; }
        
        public int UserId { get; set; }
        
        public int RoleId { get; set; }
        
        public Project Project { get; set; }
        
        public User User { get; set; }
        
        public Role Role { get; set; }
    }
}