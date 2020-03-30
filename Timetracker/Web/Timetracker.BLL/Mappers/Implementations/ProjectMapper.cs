using Timetracker.Entities.Data;
using Timetracker.Models.Data;
using Timetracker.BLL.Mappers.Interfaces;
using System.Linq;

namespace Timetracker.BLL.Mappers.Implementations
{
    public class ProjectMapper : IMapper<ProjectEntity, ProjectModel>
    {
        private readonly IMapper<UserEntity, UserModel> _userMapper;

        public ProjectMapper(IMapper<UserEntity, UserModel> userMapper)
        {
            _userMapper = userMapper;
        }

        public ProjectEntity Map(ProjectModel source)
        {
            return new ProjectEntity
            {
                Id = source.Id,
                Title = source.Title,
                Color = source.Color,
                UserProjects = source.Members?.Select(member => new UserProjectsEntity
                {
                    UserId = member.Id,                    
                    ProjectId = source.Id,
                }).ToList(),
            };
        }

        public ProjectModel Map(ProjectEntity source)
        {
            return new ProjectModel
            {
                Id = source.Id,
                Title = source.Title,
                Color = source.Color,
                Members = source.UserProjects?.Select(up => up.User != null ? _userMapper.Map(up.User) : null).ToList()
            };
        }
    }
}