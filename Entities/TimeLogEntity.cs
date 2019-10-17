using System;
using System.ComponentModel.DataAnnotations;

namespace IMS_Timetracker.Entities
{
    public class TimeLog
    {
        public int Id { get; set; }
        
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }

        public string Description { get; set; }
        
        public float Hours { get; set; }

        [Timestamp]
        public DateTime TimeStart { get; set; }
        
        [Timestamp]
        public DateTime TimeEnd { get; set; }
    }
}