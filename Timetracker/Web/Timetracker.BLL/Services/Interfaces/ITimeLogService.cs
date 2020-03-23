using System.Collections.Generic;
using System.Threading.Tasks;
using Timetracker.Models.Data;

namespace Timetracker.BLL.Services.Interfaces
{
    public interface ITimeLogService
    {
        Task<TimeLogModel> CreateTimeLog(TimeLogModel timeLog);

        Task<bool> RemoveTimeLog(int timeLogId);

        Task<TimeLogModel> UpdateTimeLog(TimeLogModel timeLog);

        Task<List<TimeLogModel>> GetTimeLogList(TimeLogFilter timeLogFilter);
    }
}
