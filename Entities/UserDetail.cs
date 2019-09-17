using System.Collections.Generic;
using IMS_Timetracker.Enums;

namespace IMS_Timetracker.Entities
{
    public class UserDetail
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get;set; }
    }
}
