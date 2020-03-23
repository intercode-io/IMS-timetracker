using System.Collections.Generic;

namespace Timetracker.Entities.Data
{
    public class RoleEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ProjectUserRoleEntity> ProjectsUsersRoles { get; set; }

        public ICollection<RolePermissionEntity> RolesPermissions { get; set; }
    }
}