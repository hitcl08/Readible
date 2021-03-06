using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Readible.Domain.Interfaces;
using Readible.Requests;
using System.Threading.Tasks;

namespace Readible.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _userService.GetUsers();
            if (result.Count == 0)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet("{username}")]
        public IActionResult Get([FromRoute] string username)
        {
            var result = _userService.GetUserByUsername(username);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRequest request)
        {
            var result = await _userService.AddUser(request.Username, request.Password);
            if (!result)
            {
                return BadRequest(result);
            }

            return Created("user", result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _userService.DeleteUser(id);
            if (!result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUserRequest request)
        {
            var result = await _userService.UpdateUserPassword(request.UserId, request.Password);
            if (!result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
