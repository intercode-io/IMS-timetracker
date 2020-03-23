using System;

namespace Timetracker.Models.Data
{
    public class TimeLogModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int ProjectId { get; set; }

        public string ProjectTitle { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int Duration { get; set; }

        public string Color { get; set; }

        public string Logs { get; set; }
    }
}