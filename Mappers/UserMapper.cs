using IMS_Timetracker.Abstraction;
using IMS_Timetracker.Entities;
using IMS_Timetracker.Dto;
using UserDto = IMS_Timetracker.Dto.User;

namespace IMS_Timetracker.Mappers
{
    public class UserMapper: IMapper<UserEntity, UserDto>
    {
        public UserEntity Map(UserDto source)
        {
            return new UserEntity
            {
                Id = source.Id,
                FirstName = source.FirstName
            };
        }

        public UserDto Map(UserEntity source)
        {
            return new UserDto
            {
                Id = source.Id,
                FirstName = source.FirstName
            };
        }
    }
}