using System.Collections.Generic;
using Timetracker.Entities.Constants;
using Timetracker.Models.Data;

namespace Timetracker.Models.Data
{
    public class ProjectUserRoleModel
    {
        public int Id { get; set; }

        public ProjectModel Project { get; set; }

        public UserModel User { get; set; }
    }
}