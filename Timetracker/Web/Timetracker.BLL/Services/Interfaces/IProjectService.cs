using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Timetracker.Models.Data;

namespace Timetracker.BLL.Services.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectModel> CreateProject(ProjectModel project);

        Task<List<ProjectModel>> GetProjects();

        Task<List<ProjectModel>> GetUserProjects(ClaimsPrincipal user);

        Task<ProjectModel> UpdateProject(ProjectModel project);

        Task<bool> RemoveProject(int projectId);
    }
}
