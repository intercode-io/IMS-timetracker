using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Timetracker.BLL.Exceptions;
using Timetracker.BLL.Services.Interfaces;
using Timetracker.Models.Data;

namespace Timetracker.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/auth")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpGet("googleLogin")]
        public async Task<IActionResult> GoogleExternalLogin(string googleIdToken)
        {
            try
            {
                var res = await _authenticationService.HandleGoogleLogin(googleIdToken);

                return Ok(res);
            }
            catch (UnsupportedDomainException ex)
            {
                return BadRequest(new Error(ex.Message, ex.GetType()));
            }
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogoutUser()
        {
            await _authenticationService.HandleLogout();

            return Ok();
        }

        [HttpGet("getCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var res = await _authenticationService.GetCurrentUser(User);

            return Ok(res);
        }
    }
}
