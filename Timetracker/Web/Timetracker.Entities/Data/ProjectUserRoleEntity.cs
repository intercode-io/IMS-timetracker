using System.Collections.Generic;

namespace Timetracker.Entities.Data
{
    public class ProjectUserRoleEntity
    {

        public int Id { get; set; }

        public int? ProjectId { get; set; }

        public ProjectEntity ProjectEntity { get; set; }

        public int UserId { get; set; }

        public UserEntity UserEntity { get; set; }

        public int RoleId { get; set; }

        public RoleEntity Role { get; set; }

        public ICollection<TimeLogEntity> TimeLogs { get; set; }
    }
}