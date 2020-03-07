using System;
using Timetracker.BLL.Mappers.Interfaces;
using Timetracker.Entities.Data;
using Timetracker.Models.Data;

namespace Timetracker.BLL.Mappers.Implementations
{
    public class TimeLogMapper : IMapper<TimeLogEntity, TimeLogModel>
    {
        public TimeLogEntity Map(TimeLogModel source)
        {
            return new TimeLogEntity()
            {
                Id = source.Id,
                ProjectUserRoleId = source.ProjectUserRoleId,
                Description = source.Description,
                Logs = source.Logs,
                Duration = source.Duration,
                Date = DateTime.Parse(source.Date, null, System.Globalization.DateTimeStyles.RoundtripKind),
            };
        }

        public TimeLogModel Map(TimeLogEntity source)
        {
            return new TimeLogModel
            {
                Id = source.Id,
                Logs = source.Logs,
                Date = source.Date.ToString("g"),
                Duration = source.Duration
            };
        }
    }
}