using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IMS_Timetracker.Abstraction;
using IMS_Timetracker.Context;
using IMS_Timetracker.Entities;
using IMS_Timetracker.Exceptions;
using IMS_Timetracker.Entities.Privileges;
using IMS_Timetracker.Dto;

namespace IMS_Timetracker.Services
{
    public interface IProjectService
    {
        Task<Project> CreateProject(Project project);
        Task<List<Project>> GetProjectList(int userId);
        Task<bool> UpdateProject(Project project);
    }

    public class ProjectService : IProjectService
    {
        private readonly TimetrackerDbContext _context;
        private readonly IMapper<ProjectEntity, Project> _projectMapper;

        public ProjectService(
            TimetrackerDbContext context,
            IMapper<ProjectEntity, Project> projectMapper
        )
        {
            _context = context;
            _projectMapper = projectMapper;
        }

        public async Task<Project> CreateProject(Project project)
        {
            try
            {
                ProjectEntity projectEntityEntity = _projectMapper.Map(project);
                var projectDb = await _context.Projects.AddAsync(projectEntityEntity);
                await _context.SaveChangesAsync();

                if (projectEntityEntity.ProjectsUsersRoles == null)
                {
                    projectEntityEntity.ProjectsUsersRoles = new List<ProjectUserRole>();
                }

                projectEntityEntity.ProjectsUsersRoles.Add(new ProjectUserRole
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

        public async Task<bool> UpdateProject(Project project)
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

        public async Task<List<Project>> GetProjectList(int userId)
        {
            List<Project> projectList = await _context.Projects
                .Include(pur => pur.ProjectsUsersRoles)
                .Where(purs => purs.ProjectsUsersRoles.Any(att => att.UserId == userId))
                .Select(p => new Project
                {
                    Id = p.Id,
                    Title = p.Title,
                    Color = p.Color
                }).ToListAsync();

            return projectList;
        }
    }
}