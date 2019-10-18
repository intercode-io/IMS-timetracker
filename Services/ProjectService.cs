using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMS_Timetracker.Abstraction;
using IMS_Timetracker.Context;
using IMS_Timetracker.Entities;
using IMS_Timetracker.Exceptions;
using IMS_Timetracker.Mappers;
using Microsoft.AspNetCore.Rewrite.Internal.ApacheModRewrite;
using IMS_Timetracker.Entities.Privileges;
using IMS_Timetracker.Enums;
using Microsoft.EntityFrameworkCore;

namespace IMS_Timetracker.Services
{
    public interface IProjectService
    {
        Task<Dto.Project> CreateProject(Dto.Project project);
        Task<List<Dto.Project>> GetProjectList(int userId);
    }
    
    public class ProjectService: IProjectService
    {
        private readonly TimetrackerDbContext _context;
        private readonly IMapper<ProjectEntity, Dto.Project> _projectMapper;

        public ProjectService(
            TimetrackerDbContext context,
            IMapper<ProjectEntity, Dto.Project> projectMapper
        )
        {
            _context = context;
            _projectMapper = projectMapper;
        }

        public async Task<Dto.Project> CreateProject(Dto.Project project)
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
                    UserId = 1
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

        public async Task<List<Dto.Project>> GetProjectList(int userId)
        {
            List<Dto.Project> projectList = await _context.Projects
                .Include(pur => pur.ProjectsUsersRoles)
                .Where(purs => purs.ProjectsUsersRoles.Any(att => att.UserId == userId))
                .Select(p => new Dto.Project
                {
                    Id = p.Id,
                    Title = p.Title
                }).ToListAsync();
            
            return projectList;
//            return new List<Dto.Project>();
        }

//        public async Task GetProjectsUserTimelogList(int[] projectIds, int userId)
//        {
//            
//        }
    }
}