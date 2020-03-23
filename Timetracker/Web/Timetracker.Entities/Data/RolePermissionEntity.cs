using Timetracker.Entities.Constants;

namespace Timetracker.Entities.Data
{
    public class RolePermissionEntity
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public RoleEntity Role { get; set; }

        public Permissions Permission { get; set; }
    }
}