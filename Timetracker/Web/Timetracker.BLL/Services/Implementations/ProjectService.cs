using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<UserEntity> _userManager;

        public ProjectService(
            TimetrackerDbContext context,
            IMapper<ProjectEntity, ProjectModel> projectMapper,
            UserManager<UserEntity> userManager
        )
        {
            _context = context;
            _projectMapper = projectMapper;

            _userManager = userManager;
        }

        public async Task<List<ProjectModel>> GetProjects(ClaimsPrincipal user)
        {
            var userId = int.Parse(_userManager.GetUserId(user));

            var projectsList = await _context.UserProjects
                    .Include(up => up.Project)
                    .Where(up => up.UserId == userId)
                    .Select(up => _projectMapper.Map(up.Project))
                    .ToListAsync();

            return projectsList;
        }

        public async Task<ProjectModel> CreateProject(ProjectModel projectModel, ClaimsPrincipal user)
        {
            var userId = int.Parse(_userManager.GetUserId(user));

            try
            {
                var projectEntity = _projectMapper.Map(projectModel);

                await _context.Projects.AddAsync(projectEntity);
                await _context.UserProjects.AddAsync(new UserProjectsEntity
                {
                    UserId = userId,
                    ProjectId = projectEntity.Id,
                });
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

            projectEntity.Title = project.Title;
            projectEntity.Color = project.Color;

            _context.Projects.Update(projectEntity);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<bool> RemoveProject(int projectId)
        {
            var projectEntityToRemove = await _context.Projects
                    .Include(p => p.UserProjects)
                    .FirstOrDefaultAsync(p => p.Id == projectId);

            if (projectEntityToRemove == null)
            {
                throw new NoSuchEntityException("The entity does not exist");
            }

            _context.Projects.Remove(projectEntityToRemove);
            _context.UserProjects.RemoveRange(projectEntityToRemove.UserProjects);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}