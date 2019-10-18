using System;
using IMS_Timetracker.Abstraction;
using IMS_Timetracker.Entities;
using IMS_Timetracker.Dto;
using UserDto = IMS_Timetracker.Dto.User;

namespace IMS_Timetracker.Mappers
{
    public class TiemLogMapper: IMapper<TimeLogEntity, TimeLog>
    {
        public TimeLogEntity Map(TimeLog source)
        {
            return new TimeLogEntity()
            {
                Id = source.Id,
                ProjectUserRoleId = source.ProjectUserRoleId,
                Description = source.Description,
                Hours = source.Hours,
                
                //TimeStart = DateTime.ParseExact(source.TimeStart, "yyyy-MM-dd HH:mm:ss",null),
                //TimeEnd = DateTime.ParseExact(source.TimeEnd, "yyyy-MM-dd HH:mm:ss",null)
                TimeStart = DateTime.Parse(source.TimeStart, null, System.Globalization.DateTimeStyles.RoundtripKind),
                TimeEnd = DateTime.Parse(source.TimeEnd, null, System.Globalization.DateTimeStyles.RoundtripKind)
            };
        }

        public TimeLog Map(TimeLogEntity source)
        {
            return new TimeLog
            {
                Id = source.Id,
                ProjectUserRoleId = source.ProjectUserRoleId,
                Hours = source.Hours,
                TimeStart = source.TimeStart.ToString("g"),
                TimeEnd = source.TimeEnd.ToString("g")
            };
        }
    }
}