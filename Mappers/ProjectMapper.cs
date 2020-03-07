using IMS_Timetracker.Abstraction;
using IMS_Timetracker.Entities;
using IMS_Timetracker.Dto;

namespace IMS_Timetracker.Mappers
{
    public class ProjectMapper : IMapper<ProjectEntity, Project>
    {
        public ProjectEntity Map(Project source)
        {
            return new ProjectEntity
            {
                Id = source.Id,
                Title = source.Title,
                Color = source.Color,
            };
        }

        public Project Map(ProjectEntity source)
        {
            return new Project
            {
                Id = source.Id,
                Title = source.Title,
                Color = source.Color,
            };
        }
    }
}