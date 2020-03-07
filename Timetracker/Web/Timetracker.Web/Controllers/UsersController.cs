using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timetracker.BLL.Exceptions;
using Timetracker.BLL.Services.Interfaces;
using Timetracker.Models.Data;

namespace Timetracker.Web.Controllers
{
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

        [HttpGet("get/{id}")]
        public async Task<ActionResult> GetUser(int id)
        {
            try
            {
                UserModel user = await _userService.GetUser(id);

                return Ok(user);
            }
            catch (NoSuchEntityException exception)
            {
                return BadRequest(new Error(exception.Message, exception.GetType()));
            }
        }
    }
}
