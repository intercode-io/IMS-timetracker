using System.Collections.Generic;

namespace IMS_Timetracker.Entities.Privileges
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ProjectUserRole> ProjectsUsersRoles { get; set; }

        public ICollection<RolePermission> RolesPermissions { get; set; }
    }
}