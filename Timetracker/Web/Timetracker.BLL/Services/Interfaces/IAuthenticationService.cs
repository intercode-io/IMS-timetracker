using System.Security.Claims;
using System.Threading.Tasks;
using Timetracker.Models.Data;

namespace Timetracker.BLL.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserModel> HandleGoogleLogin(string googleIdToken);

        Task HandleLogout();

        Task<UserModel> GetCurrentUser(ClaimsPrincipal user);
    }
}
