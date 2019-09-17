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

        public ProjectController(
            IProjectService projectService
        )
        {
            _projectService = projectService;
        }
        
        // POST api/values
        [HttpPost("create")]
        public async Task<IActionResult> CreateProject([FromBody] Dto.Project projectDto)
        {
            Dto.Project project = await _projectService.CreateProject(projectDto);
            return Ok(project);
        }
    }
}
