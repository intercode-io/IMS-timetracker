using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMS_Timetracker.Abstraction;
using IMS_Timetracker.Context;
using IMS_Timetracker.Dto;
using IMS_Timetracker.Entities;
using IMS_Timetracker.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace IMS_Timetracker.Services
{
    public interface ITimeLogService 
    {
        Task<TimeLog> CreateTimeLog(TimeLog timeLog);
        Task<List<Dto.TimeLog>> GetTimeLogList(Dto.TimeLogFilter timeLogFilter);
    }
    
    public class TimeLogService : ITimeLogService
    {
        private readonly TimetrackerDbContext _context;
        private readonly IMapper<TimeLogEntity, TimeLog> _timeLogMapper;

        public TimeLogService(TimetrackerDbContext context,
            IMapper<TimeLogEntity, TimeLog> timeLogMapper)
        {
            _context = context;
            _timeLogMapper = timeLogMapper;
        }

        public async Task<TimeLog> CreateTimeLog(TimeLog timeLog)
        {
            try
            {
                TimeLogEntity timeLogEntity = _timeLogMapper.Map(timeLog);
                var timeLogDb = await _context.TimeLogs.AddAsync(timeLogEntity);
                await _context.SaveChangesAsync();
                return _timeLogMapper.Map(timeLogEntity);
            }
            catch (Exception exception)
            {
                throw new CouldNotSaveException("Can't create new TimeLog.'", exception.Message);
            }
        }
        
        public async Task<List<Dto.TimeLog>> GetTimeLogList(Dto.TimeLogFilter timeLogFilter)
        {
            var projectIdsFilter = timeLogFilter.ProjectIds.AsEnumerable();

            var projectIds = await _context.ProjectsUsersRoles
                .Where(p => p.UserId == timeLogFilter.UserId && 
                            projectIdsFilter.Any(f => f == p.ProjectId))
                .Select(p => p.ProjectId)
                .ToListAsync();

            
            var projectIdsArray = projectIds.AsEnumerable();

            return await _context.TimeLogs
                .Where(t => t.UserId == timeLogFilter.UserId && projectIdsArray.Contains(t.ProjectId))
                .Select(
                    t => new TimeLog
                    {
                        Id = t.Id,
                        ProjectId = t.ProjectId,
                        ProjectTitle = t.ProjectEntity.Title,
                        UserId = t.UserId,
                        Description = t.Description,
                        Hours = t.Hours,
                        TimeStart = t.TimeStart,
                        TimeEnd = t.TimeEnd
                    })
                .ToListAsync();
        }
    }
}