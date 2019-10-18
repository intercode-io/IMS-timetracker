using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMS_Timetracker.Entities;
using IMS_Timetracker.Exceptions;
using IMS_Timetracker.Services;
using Microsoft.AspNetCore.Mvc;

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
                Dto.User user = await _userServivce.GetUser(id);
                return Ok(user);
            }
            catch (NoSuchEntityException exception)
            {
                return BadRequest(new Dto.Error(exception.Message, exception.GetType()));
            }
        }
    }
}
