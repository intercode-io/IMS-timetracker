using System.Collections.Generic;
using System.Threading.Tasks;
using Timetracker.Models.Data;

namespace Timetracker.BLL.Services.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectModel> CreateProject(ProjectModel project);

        Task<List<ProjectUserRoleModel>> GetProjectUserRoleList(int userId);

        Task<ProjectModel> UpdateProject(ProjectModel project);

        Task<bool> RemoveProject(int projectId);
    }
}
