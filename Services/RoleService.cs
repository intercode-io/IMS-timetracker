using System;
using System.Threading.Tasks;
using IMS_Timetracker.Context;
using IMS_Timetracker.Dto.Privileges;
using System.Collections.Generic;
using System.Linq;
using IMS_Timetracker.Entities.Privileges;
using IMS_Timetracker.Enums;
using IMS_Timetracker.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;

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


            Dto.Privileges.ProjectUserPermissions permsDto = new Dto.Privileges.ProjectUserPermissions
            {
                ProjectId = projectId,
                UserId = userId,
                Permissions = perms.ToList().Select(p => p.Permission).ToList()
                
            };
            return permsDto;
        }
    }
}