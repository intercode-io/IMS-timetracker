using Timetracker.BLL.Mappers.Interfaces;
using Timetracker.Entities.Data;
using Timetracker.Models.Data;

namespace Timetracker.BLL.Mappers.Implementations
{
    public class ProjectUserRoleMapper : IMapper<ProjectUserRoleEntity, ProjectUserRoleModel>
    {
        private readonly IMapper<ProjectEntity, ProjectModel> _projectMapper;
        private readonly IMapper<UserEntity, UserModel> _userMapper;

        public ProjectUserRoleMapper(IMapper<ProjectEntity, ProjectModel> projectMapper, IMapper<UserEntity, UserModel> userMapper)
        {
            _projectMapper = projectMapper;
            _userMapper = userMapper;
        }

        public ProjectUserRoleEntity Map(ProjectUserRoleModel source)
        {
            return new ProjectUserRoleEntity
            {
                Id = source.Id,
                ProjectEntity = _projectMapper.Map(source.Project),
                UserEntity = _userMapper.Map(source.User),
            };
        }

        public ProjectUserRoleModel Map(ProjectUserRoleEntity source)
        {
            return new ProjectUserRoleModel
            {
                Id = source.Id,
                Project = _projectMapper.Map(source.ProjectEntity),
                User = _userMapper.Map(source.UserEntity),
            };
        }
    }

}