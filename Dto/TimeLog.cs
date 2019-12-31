using System;
using System.ComponentModel.DataAnnotations;

namespace IMS_Timetracker.Dto
{
    public class TimeLog
    {
        public int Id { get; set; }
        
        public int ProjectUserRoleId { get; set; }
        public string UserName { get; set; }
        public int? ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Logs { get; set; }
        
        public float Hours { get; set; }

        public string TimeStart { get; set; }
        
        public string TimeEnd { get; set; }

    }
}