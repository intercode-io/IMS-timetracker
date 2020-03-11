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
        private readonly IMapper<ProjectUserRoleEntity, ProjectUserRoleModel> _projectUserRoleMapper;

        public ProjectService(
            TimetrackerDbContext context,
            IMapper<ProjectEntity, ProjectModel> projectMapper,
            IMapper<ProjectUserRoleEntity, ProjectUserRoleModel> projectUserRoleMapper
        )
        {
            _context = context;
            _projectMapper = projectMapper;
            _projectUserRoleMapper = projectUserRoleMapper;
        }

        public async Task<List<ProjectUserRoleModel>> GetProjectUserRoleList(int userId)
        {
            var projectUserRoleList = await _context.ProjectsUsersRoles
                    .Include(pur => pur.ProjectEntity)
                    .Include(pur => pur.Role)
                    .Include(pur => pur.UserEntity)
                    .Where(pur => pur.UserId == userId)
                    .Select(pur => _projectUserRoleMapper.Map(pur))
                    .ToListAsync();

            return projectUserRoleList;
        }

        public async Task<ProjectModel> CreateProject(ProjectModel projectModel)
        {
            try
            {
                var projectEntity = _projectMapper.Map(projectModel);

                if (projectEntity.ProjectsUsersRoles == null)
                {
                    projectEntity.ProjectsUsersRoles = new List<ProjectUserRoleEntity>();
                    projectEntity.ProjectsUsersRoles.Add(new ProjectUserRoleEntity
                    {
                        ProjectId = projectEntity.Id,
                        RoleId = 1,
                        UserId = 2
                    });
                }

                await _context.Projects.AddAsync(projectEntity);
                await _context.SaveChangesAsync();

                return _projectMapper.Map(projectEntity);
            }
            catch (Exception exception)
            {
                throw new CouldNotSaveException("Can't create new project.", exception.Message);
            }
        }

        public async Task<ProjectModel> UpdateProject(ProjectModel project)
        {
            var projectEntity = await _context.Projects.FirstOrDefaultAsync(a => a.Id == project.Id);

            if (projectEntity == null)
            {
                throw new NoSuchEntityException("The entity does not exist");
            }

            if (project != null)
            {
                projectEntity.Title = project.Title;
                projectEntity.Color = project.Color;

                _context.Projects.Update(projectEntity);
                await _context.SaveChangesAsync();
            }

            return project;
        }

        public async Task<bool> RemoveProject(int projectId)
        {
            var projectEntityToRemove = await _context.Projects
                    .Include(p => p.ProjectsUsersRoles)
                    .FirstOrDefaultAsync(p => p.Id == projectId);

            if (projectEntityToRemove == null)
            {
                throw new NoSuchEntityException("The entity does not exist");
            }

            _context.Projects.Remove(projectEntityToRemove);
            _context.ProjectsUsersRoles.RemoveRange(projectEntityToRemove.ProjectsUsersRoles);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}