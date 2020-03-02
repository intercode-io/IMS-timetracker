using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IMS_Timetracker.Entities.Privileges;

namespace IMS_Timetracker.Entities
{
    public class TimeLogEntity
    {
        public int Id { get; set; }
        
        public int ProjectUserRoleId { get; set; }
        public ProjectUserRole ProjectUserRole { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public string Logs { get; set; }
        public string Description { get; set; }
    }
}