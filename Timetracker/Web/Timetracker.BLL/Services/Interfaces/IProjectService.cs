using System.Collections.Generic;
using System.Threading.Tasks;
using Timetracker.Models.Data;

namespace Timetracker.BLL.Services.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectModel> CreateProject(ProjectModel project);

        Task<List<ProjectModel>> GetProjectList(int userId);

        Task<bool> UpdateProject(ProjectModel project);
    }
}
