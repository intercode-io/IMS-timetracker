using IMS_Timetracker.Abstraction;
using IMS_Timetracker.Entities;
using IMS_Timetracker.Dto;

namespace IMS_Timetracker.Mappers
{
    public class UserMapper : IMapper<UserEntity, User>
    {
        public UserEntity Map(User source)
        {
            return new UserEntity
            {
                Id = source.Id,
                FirstName = source.FirstName
            };
        }

        public User Map(UserEntity source)
        {
            return new User
            {
                Id = source.Id,
                FirstName = source.FirstName
            };
        }
    }
}