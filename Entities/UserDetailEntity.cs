using System.Collections.Generic;
using IMS_Timetracker.Enums;

namespace IMS_Timetracker.Entities
{
    public class UserDetailEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        //TODO add relationships public UserEntity UserEntity { get;set; }
    }
}
