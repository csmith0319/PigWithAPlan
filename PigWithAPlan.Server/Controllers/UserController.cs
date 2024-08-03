using Microsoft.AspNetCore.Mvc;
using PigWithAPlan.Server.Interfaces;
using PigWithAPlan.Server.Models;
using PigWithAPlan.Server.Services;

namespace PigWithAPlan.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            return NoContent();
        }
    }
}
