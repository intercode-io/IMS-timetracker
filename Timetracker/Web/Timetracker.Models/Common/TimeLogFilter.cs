using System.Collections.Generic;

namespace Timetracker.Models.Data
{
    public class TimeLogFilter
    {
        public List<int> ProjectIds { get; set; }

        public TimeLogDateRangeFilter ActivityDateRangeFilter { get; set; }
    }
}