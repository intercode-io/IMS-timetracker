using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timetracker.BLL.Exceptions;
using Timetracker.BLL.Services.Interfaces;
using Timetracker.Models.Data;

namespace Timetracker.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;

        public UsersController(
            IUserService userServivce
        )
        {
            _userService = userServivce;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            var res = await _userService.GetAllUsers();

            return Ok(res);
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult> GetUser(int id)
        {
            try
            {
                var result = await _userService.GetUser(id);

                return Ok(result);
            }
            catch (NoSuchEntityException exception)
            {
                return BadRequest(new Error(exception.Message, exception.GetType()));
            }
        }
    }
}
