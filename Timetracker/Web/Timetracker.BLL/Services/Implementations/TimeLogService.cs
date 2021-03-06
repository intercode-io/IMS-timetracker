﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timetracker.BLL.Services.Interfaces;
using Timetracker.BLL.Exceptions;
using Timetracker.BLL.Mappers.Interfaces;
using Timetracker.DAL.Context;
using Timetracker.Entities.Data;
using Timetracker.Models.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Timetracker.BLL.Services.Implementations
{
    public class TimeLogService : ITimeLogService
    {
        private readonly TimetrackerDbContext _context;
        private readonly IMapper<TimeLogEntity, TimeLogModel> _timeLogMapper;
        private readonly UserManager<UserEntity> _userManager;

        public TimeLogService(TimetrackerDbContext context,
            IMapper<TimeLogEntity, TimeLogModel> timeLogMapper,
            UserManager<UserEntity> userManager)
        {
            _context = context;
            _timeLogMapper = timeLogMapper;

            _userManager = userManager;
        }

        public async Task<TimeLogModel> CreateTimeLog(TimeLogModel timeLog)
        {
            try
            {
                var timeLogEntity = _timeLogMapper.Map(timeLog);

                await _context.TimeLogs.AddAsync(timeLogEntity);
                await _context.SaveChangesAsync();

                return timeLog;
            }
            catch (Exception exception)
            {
                throw new CouldNotSaveException("Can't create new TimeLog.'", exception.Message);
            }
        }

        public async Task<bool> RemoveTimeLog(int timeLogId)
        {
            var entityToRemove = _context.TimeLogs.Find(timeLogId);

            if (entityToRemove == null)
            {
                throw new NoSuchEntityException("The entity does not exist");
            }

            _context.TimeLogs.Remove(entityToRemove);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<TimeLogModel> UpdateTimeLog(TimeLogModel timeLog)
        {
            var entityToUpdate = await _context.TimeLogs
                                    .FirstOrDefaultAsync(x => x.Id == timeLog.Id);

            if (entityToUpdate == null)
            {
                throw new NoSuchEntityException("The entity does not exist");
            }

            entityToUpdate.ProjectId = timeLog.ProjectId;
            entityToUpdate.Description = timeLog.Description;
            entityToUpdate.Logs = timeLog.Logs;
            entityToUpdate.Date = timeLog.Date;
            entityToUpdate.Duration = timeLog.Duration;

            _context.TimeLogs.Update(entityToUpdate);
            await _context.SaveChangesAsync();

            return timeLog;
        }


        public async Task<List<TimeLogModel>> GetTimeLogList(TimeLogFilter timeLogFilter, ClaimsPrincipal user)
        {
            var userId = int.Parse(_userManager.GetUserId(user));
            var projectsFilter = timeLogFilter.ProjectIds;
            var timeFilter = timeLogFilter.ActivityDateRangeFilter;

            var timeLogs = await _context.TimeLogs
                                .Include(tl => tl.Project)
                                .Include(tl => tl.User)
                                .Where(tl => tl.UserId == userId && tl.Date >= timeFilter.DateFrom && tl.Date <= timeFilter.DateTo)
                                .Select(tl => _timeLogMapper.Map(tl))
                                .ToListAsync();

            if (projectsFilter.Count > 0)
            {
                timeLogs = timeLogs.Where(tl => projectsFilter.Contains((int) tl.ProjectId)).ToList();
            }

            return timeLogs;
        }
    }
}