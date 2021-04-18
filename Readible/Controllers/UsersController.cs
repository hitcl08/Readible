using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Readible.Domain.Interfaces;
using Readible.Domain.Models;
using Readible.Requests;
using Readible.ServiceModel.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Readible.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _userService.GetUsers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var result = _userService.GetUserById(id);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> Get([FromRoute] string username)
        {
            var result = _userService.GetUserByUsername(username);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRequest request)
        {
            var result = await _userService.AddUser(request.Username, request.Password);
            if (!result)
            {
                return BadRequest(result);
            }

            return Created("user",result);
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
        public async Task<IActionResult> Put([FromBody] UserRequest request)
        {
            var result = await _userService.UpdateUserPassword(request.Username, request.Password);
            if (!result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
