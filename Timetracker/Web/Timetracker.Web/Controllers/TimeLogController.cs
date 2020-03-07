using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timetracker.BLL.Services.Interfaces;
using Timetracker.Models.Data;

namespace Timetracker.Web.Controllers
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
        public async Task<ActionResult> GetTimeLog([FromBody] TimeLogFilter timeLogFilterDto)
        {
            List<TimeLogModel> timeLogs = await _timeLogService.GetTimeLogList(timeLogFilterDto);

            return Ok(timeLogs);
        }

        [HttpPost("create")]
        public async Task<ActionResult> AddTimeLog([FromBody] TimeLogModel timeLogDto)
        {
            timeLogDto.ProjectUserRoleId = 7;
            TimeLogModel timeLog = await _timeLogService.CreateTimeLog(timeLogDto);

            return Ok(timeLog);
        }

        [HttpGet("remove/{timeLogId}")]
        public async Task<IActionResult> RemoveTimeLog(int timeLogId)
        {
            bool timeLog = await _timeLogService.RemoveTimeLog(timeLogId);

            return Ok(timeLog);
        }


        [HttpPost("update")]
        public async Task<ActionResult> UpdateTimeLog([FromBody] TimeLogModel timeLog)
        {
            bool result = await _timeLogService.UpdateTimeLog(timeLog);

            return Ok(result);
        }
    }
}