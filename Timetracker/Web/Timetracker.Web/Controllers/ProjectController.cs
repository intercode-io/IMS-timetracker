using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IMS_Timetracker.Services;
using IMS_Timetracker.Dto;

namespace IMS_Timetracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        private readonly IRoleService _roleService;
        public ProjectController(
            IProjectService projectService,
            IRoleService roleService
        )
        {
            _projectService = projectService;
            _roleService = roleService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProject([FromBody] Project projectDto)
        {
            Project project = await _projectService.CreateProject(projectDto);

            return Ok(project);
        }

        [HttpPost("update")]
        public async Task<ActionResult> UpdateProject([FromBody] Project project)
        {
            bool result = await _projectService.UpdateProject(project);

            return Ok(result);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetProjectList()
        {
            int userId = 2;
            List<Project> project = await _projectService.GetProjectList(userId);

            return Ok(project);
        }

        [HttpGet("getPermissions/projectId={projectId}&userId={userId}")]
        public async Task<IActionResult> GetProjectUserPermissions(int projectId, int userId)
        {
            var res = await _roleService.GetProjectUserPermissions(projectId, userId);

            return Ok(res);
        }
    }
}
