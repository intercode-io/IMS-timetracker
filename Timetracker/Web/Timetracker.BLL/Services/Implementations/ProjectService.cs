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

        public async Task<List<ProjectModel>> GetProjects()
        {
            var projectsList = await _context.Projects
                .Include(p => p.UserProjects)
                    .ThenInclude(up => up.User)
                .Select(p => _projectMapper.Map(p))
                .ToListAsync();

            return projectsList;
        }

        public async Task<List<ProjectModel>> GetUserProjects(ClaimsPrincipal user)
        {
            var userId = int.Parse(_userManager.GetUserId(user));

            var projectsList = await _context.UserProjects
                    .Include(up => up.Project)
                    .Where(up => up.UserId == userId)
                    .Select(up => _projectMapper.Map(up.Project))
                    .ToListAsync();

            return projectsList;
        }

        public async Task<ProjectModel> CreateProject(ProjectModel projectModel)
        {
            try
            {
                var projectEntity = _projectMapper.Map(projectModel);

                await _context.Projects.AddAsync(projectEntity);
                await _context.SaveChangesAsync();

                var result = _projectMapper.Map(projectEntity);
                result.Members = projectModel.Members;

                return result;
            }
            catch (Exception exception)
            {
                throw new CouldNotSaveException("Can't create new project.", exception.Message);
            }
        }

        public async Task<ProjectModel> UpdateProject(ProjectModel project)
        {
            var existingProjectEntity = _context.Projects
                        .Include(p => p.UserProjects)
                        .FirstOrDefault(p => p.Id == project.Id);

            var existingUserProjects = existingProjectEntity?.UserProjects;

            if (existingProjectEntity == null)
            {
                throw new NoSuchEntityException("The entity does not exist");
            }

            var incomingProjectEntity = _projectMapper.Map(project);
            var incomingUserProjects = incomingProjectEntity.UserProjects;

            var userProjectsToRemove = existingUserProjects.Where(eup => incomingUserProjects.All(iup => iup.UserId != eup.UserId));
            _context.UserProjects.RemoveRange(userProjectsToRemove);
            var userProjectsToAdd = incomingUserProjects.Where(iup => existingUserProjects.All(eup => eup.UserId != iup.UserId));
            _context.UserProjects.AddRange(userProjectsToAdd);

            existingProjectEntity.Title = incomingProjectEntity.Title;
            existingProjectEntity.Color = incomingProjectEntity.Color;

            _context.Projects.Update(existingProjectEntity);
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
            await _context.SaveChangesAsync();

            return true;
        }
    }
}