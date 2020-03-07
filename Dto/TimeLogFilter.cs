namespace IMS_Timetracker.Dto
{
    public class TimeLogFilter
    {
        public int[] ProjectIds { get; set; }

        public TimeLogDateRangeFilter ActivityDateRangeFilter { get; set; }
    }
}