using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timetracker.Entities.Data
{
    public class TimeLogEntity
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public ProjectEntity Project { get; set; }

        public int UserId { get; set; }

        public UserEntity User { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }

        public int Duration { get; set; }

        public string Logs { get; set; }

        public string Description { get; set; }
    }
}