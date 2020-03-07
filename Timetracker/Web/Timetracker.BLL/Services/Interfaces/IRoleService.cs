using System.Threading.Tasks;
using Timetracker.Models.Data;

namespace Timetracker.BLL.Services.Interfaces
{
    public interface IRoleService
    {
        Task<ProjectUserPermissions> GetProjectUserPermissions(int projectId, int userId);
    }
}
