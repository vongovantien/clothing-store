using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Parameter is missing");
            }

            //if (result.code == 200)
            //{
            //    var token = _authentication.GenerateJWT(result.Data);
            //    return Ok(token);
            //}
            return NotFound();
        }

        [HttpGet("UserList")]
        [Authorize(Roles = "User")]
        public IActionResult getAllUsers()
        {
            //var result = _authentication.getUserList<User>();
            return Ok();
        }
    }
}
