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

        public async Task<ProjectUserPermissions> GetProjectUserPermissions(int projectId, int userId)
        {
            var perms = _context.ProjectsUsersRoles
                .Where(x => x.ProjectId == projectId && x.UserId == userId)
                .Include(x => x.Role)
                .ThenInclude(x => x.RolesPermissions)
                .Select(x => x.Role.RolesPermissions)
                .ToList()
                .FirstOrDefault();

            if (perms == null)
            {
                throw new NoSuchEntityException(String.Format("Can't get User's (userId = %s) permission for project (projectId = %s).", userId, projectId));
            }

            ProjectUserPermissions permsDto = new ProjectUserPermissions
            {
                ProjectId = projectId,
                UserId = userId,
                Permissions = perms.ToList().Select(p => p.Permission).ToList()

            };

            return permsDto;
        }
    }
}