using Microsoft.AspNetCore.Mvc;
using Readible.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Readible.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionsController
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionsController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        
    }
}
