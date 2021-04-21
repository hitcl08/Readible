using Microsoft.AspNetCore.Mvc;
using Readible.Domain.Interfaces;
using Readible.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Readible.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionsController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _subscriptionService.GetAllSubscriptions();
            return Ok(result);
        }

        [HttpGet("users/{id}")]
        public  IActionResult Get([FromRoute] int id)
        {
            var result = _subscriptionService.GetUserSubscription(id);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("user")]
        public async Task<IActionResult> Post([FromBody] SubscriptionRequest request)
        {
            var result = await _subscriptionService.AddUserSubscription(request.UserId);
            if (!result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUserSubscription([FromRoute] int id)
        {
            var result = await _subscriptionService.DeleteUserSubscription(id);
            if (!result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}",Name ="Delete")]
        public async Task<IActionResult> DeleteSubscription([FromRoute] int id)
        {
            var result = await _subscriptionService.DeleteSubscription(id);
            if (!result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
