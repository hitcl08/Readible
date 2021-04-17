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
            var result = _userService.GetUsers();
            var userDto = _mapper.Map<List<UserDto>>(result);
            return Ok(userDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var result = await _userService.GetUser(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRequest request)
        {
            var result = await _userService.AddUser(request.Username, request.Password);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _userService.DeleteUser(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserRequest request)
        {
            var result = await _userService.UpdateUserPassword(request.Username, request.Password);
            return Ok(result);
        }

    }
}
