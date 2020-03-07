﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IMS_Timetracker.Abstraction;
using IMS_Timetracker.Context;
using IMS_Timetracker.Dto;
using IMS_Timetracker.Entities;
using IMS_Timetracker.Exceptions;

namespace IMS_Timetracker.Services
{
    public interface ITimeLogService
    {
        Task<TimeLog> CreateTimeLog(TimeLog timeLog);

        Task<bool> RemoveTimeLog(int timeLogId);

        Task<bool> UpdateTimeLog(TimeLog timeLog);

        Task<List<TimeLog>> GetTimeLogList(TimeLogFilter timeLogFilter);
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
                _context.TimeLogs.Remove(_context.TimeLogs.Find(timeLogId));
                await _context.SaveChangesAsync();
                Console.WriteLine(timeLogId);

                return true;
            }
            catch (Exception exception)
            {
                throw new NotImplementedException();
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
                    timeLogEntity.Logs = timeLog.Logs;
                    timeLogEntity.Date = DateTime.Parse(timeLog.Date, null, System.Globalization.DateTimeStyles.RoundtripKind);
                    timeLogEntity.Duration = timeLog.Duration;
                    _context.TimeLogs.Update(timeLogEntity);
                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch (Exception exception)
            {
                throw new NotImplementedException();
            }
        }


        public async Task<List<TimeLog>> GetTimeLogList(TimeLogFilter timeLogFilter)
        {
            int userId = 2;
            var projectIdsFilter = timeLogFilter.ProjectIds.AsEnumerable();

            Dictionary<int, Tuple<int?, string>> allIds = new Dictionary<int, Tuple<int?, string>>();

            List<int> projectUserRoleIds = new List<int>();

            if (timeLogFilter.ProjectIds.Count() <= 0)
            {
                allIds = await _context.ProjectsUsersRoles
                    .Include(pur => pur.ProjectEntity)
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
                    .Include(pur => pur.ProjectEntity)
                    .Where(p => p.UserId == userId &&
                                projectIdsFilter.Any(f => f == p.ProjectId))
                    .Select(p => new
                    {
                        purId = p.Id,
                        projects = Tuple.Create(p.ProjectId, p.ProjectEntity.Title)
                    })
                    .ToDictionaryAsync(d => d.purId, d => d.projects);
            }

            var purIds = allIds.Keys.ToArray();

            return await _context.TimeLogs
                .Where(t => purIds.Contains(t.ProjectUserRoleId) && t.Date >= timeLogFilter.ActivityDateRangeFilter.DateFrom
                                                                 && t.Date <= timeLogFilter.ActivityDateRangeFilter.DateTo)
                .Select(
                    t => new TimeLog
                    {
                        Id = t.Id,
                        UserName = t.ProjectUserRole.UserEntity.FirstName,
                        ProjectId = allIds[t.ProjectUserRoleId].Item1,
                        ProjectTitle = allIds[t.ProjectUserRoleId].Item2,
                        Duration = t.Duration,
                        Description = t.Description,
                        Logs = t.Logs,
                        Date = t.Date.ToString("g"),
                        Color = t.ProjectUserRole.ProjectEntity.Color
                    })
                .ToListAsync();
        }
    }
}