using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMS_Timetracker.Dto;
using IMS_Timetracker.Exceptions;
using IMS_Timetracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace IMS_Timetracker.Controllers
{
    [Route("api/timeLog")]
    [ApiController]
    
    public class TimeLogController : ControllerBase
    {
        private readonly ITimeLogService _timeLogService;

        public TimeLogController(
            ITimeLogService timeLogFilterService)
        {
            _timeLogService = timeLogFilterService;
        }


        [HttpPost("getList")]
                public async Task<ActionResult> GetTimeLog([FromBody] Dto.TimeLogFilter timeLogFilterDto)
                {
                    List<Dto.TimeLog> timeLogs = await _timeLogService.GetTimeLogList(timeLogFilterDto);
                    return Ok(timeLogs);
                }
                
        [HttpPost("create")]
                public async Task<ActionResult> AddTimeLog([FromBody] TimeLog timeLogDto)
                {
                    timeLogDto.ProjectUserRoleId=7;
                    TimeLog timeLog = await _timeLogService.CreateTimeLog(timeLogDto);
                    return Ok(timeLog);
                }
                
        [HttpGet("remove/{timeLogId}")]
                public async Task<IActionResult> RemoveTimeLog(int timeLogId)
                {
                    bool timeLog = await _timeLogService.RemoveTimeLog(timeLogId);
                    return Ok(timeLog);
                }
                
        
        [HttpPost("update")]
            public async Task<ActionResult> UpdateTimeLog([FromBody] TimeLog timeLog)
            {
                bool result = await _timeLogService.UpdateTimeLog(timeLog);
                return Ok(result);
            }
                
    }
}