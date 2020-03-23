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
                ProjectId = source.ProjectId,
                UserId = source.UserId,
                Description = source.Description,
                Logs = source.Logs,
                Duration = source.Duration,
                Date = source.Date
            };
        }

        public TimeLogModel Map(TimeLogEntity source)
        {
            return new TimeLogModel
            {
                Id = source.Id,
                Color = source.Project?.Color,
                ProjectId = source.ProjectId,
                ProjectTitle = source.Project?.Title,
                UserId = source.UserId,
                UserName = source.User?.FirstName,
                Description = source.Description,
                Logs = source.Logs,
                Duration = source.Duration,
                Date = source.Date,
            };
        }
    }
}