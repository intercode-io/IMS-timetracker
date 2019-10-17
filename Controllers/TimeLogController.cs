    
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
        private readonly TimeLogService _timeLogService;

        public TimeLogController(
            TimeLogService timeLogFilterService)
        {
            _timeLogService = timeLogFilterService;
        }


        [HttpPost("getList")]
                public async Task<ActionResult> GetTimeLog([FromBody] Dto.TimeLogFilter timeLogFilterDto)
                {
                    List<Dto.TimeLog> timeLogs = await _timeLogService.GetTimeLogList(timeLogFilterDto);
                    return Ok(timeLogs);
                }
    }
}