using IMS_Timetracker.Enums;

namespace IMS_Timetracker.Entities.Privileges
{
    public class RolePermission
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public Permissions Permission { get; set; }
    }
}