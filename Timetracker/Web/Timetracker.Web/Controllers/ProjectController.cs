using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timetracker.BLL.Services.Interfaces;
using Timetracker.Models.Data;

namespace Timetracker.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpGet("getProjects")]
        public async Task<IActionResult> GetProjects()
        {
            var result = await _projectService.GetProjects();

            return Ok(result);
        }

        [HttpGet("getUserProjects")]
        public async Task<IActionResult> GetUserProjects()
        {
            var result = await _projectService.GetUserProjects(User);

            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProject([FromBody] ProjectModel projectModel)
        {
            var result = await _projectService.CreateProject(projectModel);

            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateProject([FromBody] ProjectModel projectModel)
        {
            var result = await _projectService.UpdateProject(projectModel);

            return Ok(result);
        }

        [HttpDelete("remove/{projectId}")]
        public async Task<ActionResult> RemoveProject(int projectId)
        {
            var result = await _projectService.RemoveProject(projectId);

            return Ok(result);
        }
    }
}
