using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timetracker.BLL.Exceptions;
using Timetracker.BLL.Mappers.Interfaces;
using Timetracker.BLL.Services.Interfaces;
using Timetracker.DAL.Context;
using Timetracker.Entities.Data;

using Timetracker.Models.Data;

namespace Timetracker.BLL.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly TimetrackerDbContext _context;
        private readonly IMapper<UserEntity, UserModel> _userMapper;

        public UserService(
            TimetrackerDbContext context,
            IMapper<UserEntity, UserModel> userMapper
        )
        {
            _context = context;
            _userMapper = userMapper;
        }

        public async Task<UserModel> GetUser(int id)
        {
            UserEntity userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (userEntity != null)
            {
                return _userMapper.Map(userEntity);
            }

            throw new NoSuchEntityException("[User Service] Could not get User from db context.");
        }
    }
}