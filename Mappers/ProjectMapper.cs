using IMS_Timetracker.Abstraction;
using IMS_Timetracker.Entities;
using IMS_Timetracker.Dto;
using ProjectDto = IMS_Timetracker.Dto.Project;

namespace IMS_Timetracker.Mappers
{
    public class ProjectMapper: IMapper<ProjectEntity, ProjectDto>
    {
        public ProjectEntity Map(ProjectDto source)
        {
            return new ProjectEntity
            {
                Id = source.Id,
                Title = source.Title
            };
        }

        public ProjectDto Map(ProjectEntity source)
        {
            return new ProjectDto
            {
                Id = source.Id,
                Title = source.Title
            };
        }
    }
}