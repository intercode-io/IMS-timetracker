using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMS_Timetracker.Abstraction;
using IMS_Timetracker.Context;
using IMS_Timetracker.Dto;
using IMS_Timetracker.Entities;
using IMS_Timetracker.Entities.Privileges;
using IMS_Timetracker.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMS_Timetracker.Services
{
    public interface ITimeLogService
    {
        Task<TimeLog> CreateTimeLog(TimeLog timeLog);
        Task<bool> RemoveTimeLog(int timeLogId);
        Task<bool> UpdateTimeLog(TimeLog timeLog);
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

        public async Task<bool> RemoveTimeLog(int timeLogId)
        {
            try
            {
//                TimeLogEntity timeLogEntity = _timeLogMapper.Map(timeLog);
                _context.TimeLogs.Remove(_context.TimeLogs.Find(timeLogId));
                await _context.SaveChangesAsync();
                Console.WriteLine(timeLogId);
                return true;
//                return _timeLogMapper.Map(timeLogEntity);
            }
            catch (Exception exception)
            {
                throw new NotImplementedException();
//                throw new CouldNotSaveException("Can't create new TimeLog.'", exception.Message);

            }
        }
        
        public async Task<bool> UpdateTimeLog(TimeLog timeLog)
                {
                    try
                    {
                        TimeLogEntity timeLogEntity = _context.TimeLogs.First(a => a.Id == timeLog.Id);

                        if (timeLogEntity != null)
                        {
                            timeLogEntity.Description = timeLog.Description;
                            timeLogEntity.Hours = timeLog.Hours;
                            timeLogEntity.TimeStart = DateTime.Parse(timeLog.TimeStart, null, System.Globalization.DateTimeStyles.RoundtripKind);
                            timeLogEntity.TimeEnd = DateTime.Parse(timeLog.TimeEnd, null, System.Globalization.DateTimeStyles.RoundtripKind);
                            _context.TimeLogs.Update(timeLogEntity);
                            await _context.SaveChangesAsync();
                            return true;
                        }

                        return false;
                    }
                    catch (Exception exception)
                    {
                        throw new NotImplementedException();
        //                throw new CouldNotSaveException("Can't create new TimeLog.'", exception.Message);
        
                    }
                }

        
        public async Task<List<TimeLog>> GetTimeLogList(TimeLogFilter timeLogFilter)
        {
            int userId = 2;
            var projectIdsFilter = timeLogFilter.ProjectIds.AsEnumerable();
            
//            Dictionary<int, int?> allIds = new Dictionary<int, int?>();
            Dictionary<int, Tuple<int?, string>> allIds = new Dictionary<int, Tuple<int?, string>>();
//            Dictionary<int, Dictionary<int?, string>> allIds = new Dictionary<int, Dictionary<int?, string>>();
            
//            List<int> purIds = new List<int>();

            List<int> projectUserRoleIds = new List<int>();

//            projectUserRoleIds = await _context.ProjectsUsersRoles
//                .Where(p => p.UserId == timeLogFilter.UserId && projectIdsFilter.Any(f => f == p.ProjectId))
//                .Select(p => p.Id)
//                .ToListAsync();
            
            if (timeLogFilter.ProjectIds.Count() <= 0)
            {
                allIds = await _context.ProjectsUsersRoles
                    .Include( pur => pur.ProjectEntity)
                    .Where(p => p.UserId == userId)
                    .Select(p => new
                    {
                        purId = p.Id,
                        projects = Tuple.Create(p.ProjectId, p.ProjectEntity.Title)
                    })
                    .ToDictionaryAsync(d => d.purId, d => d.projects);
            }
            else
            {
                allIds = await _context.ProjectsUsersRoles
                    .Include( pur => pur.ProjectEntity)
                    .Where(p => p.UserId == userId &&
                                projectIdsFilter.Any(f => f == p.ProjectId))
                    .Select(p => new
                    {
                        purId = p.Id,
                        projects = Tuple.Create(p.ProjectId, p.ProjectEntity.Title)
                    })
                    .ToDictionaryAsync(d => d.purId, d => d.projects);
            }

//            var projectIdsArray = purIds.AsEnumerable();
            var purIds = allIds.Keys.ToArray();

            return await _context.TimeLogs
                .Where(t => purIds.Contains(t.ProjectUserRoleId) && t.TimeStart >= timeLogFilter.ActivityDateRangeFilter.DateFrom
                                                                 && t.TimeStart <= timeLogFilter.ActivityDateRangeFilter.DateTo)
                .Select(
                    t => new TimeLog
                    {
                        Id = t.Id,
                        UserName = t.ProjectUserRole.UserEntity.FirstName,
                        ProjectId = allIds[t.ProjectUserRoleId].Item1,
                        ProjectTitle = allIds[t.ProjectUserRoleId].Item2,
                        
                        Description = t.Description,
                        Hours = t.Hours,
                        TimeStart = t.TimeStart.ToString("g"),
                        TimeEnd = t.TimeEnd.ToString("g")
                    })
                .ToListAsync();
        }
    }
}
//}            
//return await _context.TimeLogs
//                .Where(t => t.UserId == timeLogFilter.UserId && projectIdsArray.Contains(t.ProjectId) 
//                                                             && t.TimeStart >= timeLogFilter.DateFrom 
//                                                             && t.TimeStart <= timeLogFilter.DateTo)
//                .Select(
//                    t => new TimeLog
//                    {
//                        Id = t.Id,
//                        ProjectId = t.ProjectId,
//                        ProjectTitle = t.ProjectEntity.Title,
//                        UserId = t.UserId,
//                        Description = t.Description,
//                        Hours = t.Hours,
//                        TimeStart = t.TimeStart.ToString("g"),
//                        TimeEnd = t.TimeEnd.ToString("g")
//                    })
//                .ToListAsync();
//        }
//    }
//}