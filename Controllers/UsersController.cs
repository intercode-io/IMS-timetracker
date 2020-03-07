using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IMS_Timetracker.Exceptions;
using IMS_Timetracker.Services;
using IMS_Timetracker.Dto;

namespace IMS_Timetracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserServivce _userServivce;

        public UsersController(
            UserServivce userServivce
        )
        {
            _userServivce = userServivce;
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult> GetUser(int id)
        {
            try
            {
                User user = await _userServivce.GetUser(id);

                return Ok(user);
            }
            catch (NoSuchEntityException exception)
            {
                return BadRequest(new Error(exception.Message, exception.GetType()));
            }
        }
    }
}
