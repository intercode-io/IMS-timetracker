using Timetracker.Entities.Data;
using Timetracker.Models.Data;
using Timetracker.BLL.Mappers.Interfaces;

namespace Timetracker.BLL.Mappers.Implementations
{
    public class ProjectMapper : IMapper<ProjectEntity, ProjectModel>
    {
        public ProjectEntity Map(ProjectModel source)
        {
            return new ProjectEntity
            {
                Id = source.Id,
                Title = source.Title,
                Color = source.Color,
            };
        }

        public ProjectModel Map(ProjectEntity source)
        {
            return new ProjectModel
            {
                Id = source.Id,
                Title = source.Title,
                Color = source.Color,
            };
        }
    }
}