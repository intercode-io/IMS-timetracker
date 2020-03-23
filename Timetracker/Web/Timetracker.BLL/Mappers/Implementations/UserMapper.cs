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
                FirstName = source.FirstName
            };
        }

        public UserModel Map(UserEntity source)
        {
            return new UserModel
            {
                Id = source.Id,
                FirstName = source.FirstName
            };
        }
    }
}