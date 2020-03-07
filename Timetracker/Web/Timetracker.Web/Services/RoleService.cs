using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using IMS_Timetracker.Context;
using IMS_Timetracker.Dto.Privileges;
using IMS_Timetracker.Exceptions;

namespace IMS_Timetracker.Services
{
    public interface IRoleService
    {
        Task<ProjectUserPermissions> GetProjectUserPermissions(int projectId, int userId);
    }

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