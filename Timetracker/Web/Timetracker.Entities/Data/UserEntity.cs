using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Timetracker.Entities.Data
{
    public class UserEntity : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhotoUrl { get; set; }

        public ICollection<UserProjectsEntity> UserProjects { get; set; }

        public ICollection<TimeLogEntity> TimeLogs { get; set; }
    }
}