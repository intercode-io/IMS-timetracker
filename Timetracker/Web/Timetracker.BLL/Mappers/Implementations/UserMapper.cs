using Timetracker.Entities.Data;
using Timetracker.Models.Data;
using Timetracker.BLL.Mappers.Interfaces;

namespace Timetracker.BLL.Mappers.Implementations
{
    public class UserMapper : IMapper<UserEntity, UserModel>
    {
        public UserEntity Map(UserModel source)
        {
            return new UserEntity
            {
                Id = source.Id,
                Email = source.Email,
                FirstName = source.FirstName,
                LastName = source.LastName,
                PhotoUrl = source.PhotoUrl,
            };
        }

        public UserModel Map(UserEntity source)
        {
            return new UserModel
            {
                Id = source.Id,
                Email = source.Email,
                FirstName = source.FirstName,
                LastName = source.LastName,
                PhotoUrl = source.PhotoUrl,
            };
        }
    }
}