using System.Collections.Generic;
using Timetracker.Entities.Constants;

namespace Timetracker.Models.Data
{
    public class ProjectUserRoleModel
    {
        public int ProjectId { get; set; }

        public int UserId { get; set; }

        public IEnumerable<Permissions> Permissions { get; set; }
    }
}