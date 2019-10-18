using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using IMS_Timetracker.Context;
using IMS_Timetracker.Exceptions;
using IMS_Timetracker.Services;

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
        
        // POST api/values
        [HttpPost("create")]
        public async Task<IActionResult> CreateProject([FromBody] Dto.Project projectDto)
        {
            Dto.Project project = await _projectService.CreateProject(projectDto);
            return Ok(project);
        }
        
        // POST api/values
        [HttpGet("getList/{userId}")]
        public async Task<IActionResult> GetProjectList(int userId)
        {
            List<Dto.Project> project = await _projectService.GetProjectList(userId);
            return Ok(project);
        }

        [HttpGet("getPermissions/projectId={projectId}&userId={userId}")]
        public async Task<IActionResult> GetProjectUserPermissions(int projectId, int userId)
        {
            var res = await _roleService.GetProjectUserPermissions(projectId, userId);
            return Ok(res);
        }

//        public async Task<IActionResult> GetProjectUserActivityList([FromBody] Dto.Activity.ProjectIdsActivityList projectIdsDto)
//        {
//            
//            return Ok();
//        }
    }
}
