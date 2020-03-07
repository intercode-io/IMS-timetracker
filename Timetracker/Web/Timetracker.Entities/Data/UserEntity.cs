using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Timetracker.Entities.Data
{
    public class UserEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        //TODO add relationships public UserDetail UserDetail { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public ICollection<ProjectUserRoleEntity> ProjectsUsersRoles { get; set; }
    }
}