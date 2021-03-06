﻿using System.Threading.Tasks;
using Timetracker.Models.Data;

namespace Timetracker.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> GetUser(int id);
    }
}
