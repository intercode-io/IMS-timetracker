using System.Threading.Tasks;
using IMS_Timetracker.Abstraction;
using IMS_Timetracker.Context;
using IMS_Timetracker.Entities;
using IMS_Timetracker.Exceptions;
using IMS_Timetracker.Dto;
using Microsoft.EntityFrameworkCore;

namespace IMS_Timetracker.Services
{
    public interface IUserServivce
    {
        Task<User> GetUser(int id);
    }

    public class UserServivce : IUserServivce
    {
        private readonly TimetrackerDbContext _context;
        private readonly IMapper<UserEntity, User> _userMapper;

        public UserServivce(
            TimetrackerDbContext context,
            IMapper<UserEntity, User> userMapper
        )
        {
            _context = context;
            _userMapper = userMapper;
        }

        public async Task<User> GetUser(int id)
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