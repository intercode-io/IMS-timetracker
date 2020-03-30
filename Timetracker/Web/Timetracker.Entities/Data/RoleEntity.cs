using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Timetracker.Entities.Data
{
    public class RoleEntity : IdentityRole<int>
    {
        public ICollection<RolePermissionEntity> RolesPermissions { get; set; }
    }
}