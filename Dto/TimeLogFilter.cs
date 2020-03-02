using System;

namespace IMS_Timetracker.Dto
{
    public class TimeLogFilter
    {
        public int[] ProjectIds { get; set; }
//        public DateTime DateFrom { get; set; }
//        public DateTime DateTo { get; set; }
        
        public TimeLogDateRangeFilter ActivityDateRangeFilter { get; set; }
    }
}