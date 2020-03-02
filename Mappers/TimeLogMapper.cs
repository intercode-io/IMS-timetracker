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
                Logs = source.Logs,
                Duration = source.Duration,
                // Date = DateTimeOffset.Parse(source.Date).UtcDateTime
                Date = DateTime.Parse(source.Date, null, System.Globalization.DateTimeStyles.RoundtripKind),
            };
        }

        public TimeLog Map(TimeLogEntity source)
        {
            return new TimeLog
            {
                Id = source.Id,
                Logs = source.Logs,
                Date = source.Date.ToString("g"),
                Duration = source.Duration
            };
        }
    }
}