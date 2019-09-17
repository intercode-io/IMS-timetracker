using System;
using System.Threading.Tasks;
using IMS_Timetracker.Abstraction;
using IMS_Timetracker.Context;
using IMS_Timetracker.Entities;
using IMS_Timetracker.Exceptions;
using IMS_Timetracker.Mappers;
using Microsoft.AspNetCore.Rewrite.Internal.ApacheModRewrite;

namespace IMS_Timetracker.Services
{
    public interface IProjectService
    {
        Task<Dto.Project> CreateProject(Dto.Project project);
    }
    
    public class ProjectService: IProjectService
    {
        private readonly TimetrackerDbContext _context;
        private readonly IMapper<Project, Dto.Project> _projectMapper;

        public ProjectService(
            TimetrackerDbContext context,
            IMapper<Project, Dto.Project> projectMapper
        )
        {
            _context = context;
            _projectMapper = projectMapper;
        }

        public async Task<Dto.Project> CreateProject(Dto.Project project)
        {
            try
            {
                Entities.Project projectEntity = _projectMapper.Map(project);
                var projectDb = await _context.Projects.AddAsync(projectEntity);
                await _context.SaveChangesAsync();
                return _projectMapper.Map(projectEntity);
            }
            catch (Exception exception)
            {
                throw new CouldNotSaveException("Can't create new project.", exception.Message);
            }
        }
    }
}