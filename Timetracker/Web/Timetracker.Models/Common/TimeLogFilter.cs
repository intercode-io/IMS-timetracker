namespace Timetracker.Models.Data
{
    public class TimeLogFilter
    {
        public int[] ProjectIds { get; set; }

        public TimeLogDateRangeFilter ActivityDateRangeFilter { get; set; }
    }
}