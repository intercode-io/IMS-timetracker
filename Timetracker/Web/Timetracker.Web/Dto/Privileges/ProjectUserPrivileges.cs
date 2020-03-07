using System.Collections.Generic;
using IMS_Timetracker.Enums;

namespace IMS_Timetracker.Dto.Privileges
{
    public class ProjectUserPermissions
    {
        public int ProjectId;
        public int UserId;
        public IEnumerable<Permissions> Permissions;
    }
}