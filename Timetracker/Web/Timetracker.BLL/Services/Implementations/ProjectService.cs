using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timetracker.BLL.Exceptions;
using Timetracker.BLL.Mappers.Interfaces;
using Timetracker.BLL.Services.Interfaces;
using Timetracker.DAL.Context;
using Timetracker.Entities.Data;
using Timetracker.Models.Data;

namespace Timetracker.BLL.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly TimetrackerDbContext _context;
        private readonly IMapper<ProjectEntity, ProjectModel> _projectMapper;

        public ProjectService(
            TimetrackerDbContext context,
            IMapper<ProjectEntity, ProjectModel> projectMapper
        )
        {
            _context = context;
            _projectMapper = projectMapper;
        }

        public async Task<ProjectModel> CreateProject(ProjectModel project)
        {
            try
            {
                ProjectEntity projectEntityEntity = _projectMapper.Map(project);
                var projectDb = await _context.Projects.AddAsync(projectEntityEntity);
                await _context.SaveChangesAsync();

                if (projectEntityEntity.ProjectsUsersRoles == null)
                {
                    projectEntityEntity.ProjectsUsersRoles = new List<ProjectUserRoleEntity>();
                }

                projectEntityEntity.ProjectsUsersRoles.Add(new ProjectUserRoleEntity
                {
                    ProjectId = projectEntityEntity.Id,
                    RoleId = 1,
                    UserId = 2
                });

                _context.Projects.Update(projectEntityEntity);
                await _context.SaveChangesAsync();

                return _projectMapper.Map(projectEntityEntity);
            }
            catch (Exception exception)
            {
                throw new CouldNotSaveException("Can't create new project.", exception.Message);
            }
        }

        public async Task<bool> UpdateProject(ProjectModel project)
        {
            try
            {
                ProjectEntity projectEntity = _context.Projects.First(a => a.Id == project.Id);

                if (projectEntity != null && project != null)
                {
                    projectEntity.Title = project.Title;
                    projectEntity.Color = project.Color;
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<List<ProjectModel>> GetProjectList(int userId)
        {
            List<ProjectModel> projectList = await _context.Projects
                .Include(pur => pur.ProjectsUsersRoles)
                .Where(purs => purs.ProjectsUsersRoles.Any(att => att.UserId == userId))
                .Select(p => new ProjectModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Color = p.Color
                }).ToListAsync();

            return projectList;
        }
    }
}