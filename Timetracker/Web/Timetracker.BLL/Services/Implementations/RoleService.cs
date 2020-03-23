using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Timetracker.BLL.Services.Interfaces;
using Timetracker.BLL.Exceptions;
using Timetracker.DAL.Context;
using Timetracker.Models.Data;

namespace Timetracker.BLL.Services.Implementations
{
    public class RoleService : IRoleService
    {
        protected readonly TimetrackerDbContext _context;

        public RoleService(
            TimetrackerDbContext context
        )
        {
            _context = context;
        }
    }
}