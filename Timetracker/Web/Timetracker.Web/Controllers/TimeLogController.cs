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
        public async Task<ActionResult> GetTimeLog([FromBody] TimeLogFilter timeLogFilter)
        {
            var result = await _timeLogService.GetTimeLogList(timeLogFilter);

            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult> AddTimeLog([FromBody] TimeLogModel timeLogModel)
        {
            var result = await _timeLogService.CreateTimeLog(timeLogModel);

            return Ok(result);
        }


        [HttpPut("update")]
        public async Task<ActionResult> UpdateTimeLog([FromBody] TimeLogModel timeLog)
        {
            var result = await _timeLogService.UpdateTimeLog(timeLog);

            return Ok(result);
        }

        [HttpDelete("remove/{timeLogId}")]
        public async Task<IActionResult> RemoveTimeLog(int timeLogId)
        {
            var result = await _timeLogService.RemoveTimeLog(timeLogId);

            return Ok(result);
        }
    }
}