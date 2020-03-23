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
                ProjectUserRoleId = source.ProjectUserRoleId,
                Color = source.ProjectUserRoleEntity?.ProjectEntity?.Color,
                ProjectId = source.ProjectUserRoleEntity?.ProjectId,
                ProjectTitle = source.ProjectUserRoleEntity?.ProjectEntity?.Title,
                UserName = source.ProjectUserRoleEntity?.UserEntity?.FirstName,
                Description = source.Description,
                Logs = source.Logs,
                Duration = source.Duration,
                Date = source.Date.ToString("g"),
            };
        }
    }
}