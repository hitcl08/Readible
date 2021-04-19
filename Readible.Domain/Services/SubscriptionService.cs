using Readible.Domain.Interfaces;
using Readible.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readible.Domain.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUserService _userService;


        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IUserService userService)
        {
            _subscriptionRepository = subscriptionRepository;
            _userService = userService;
        }

        public async Task<bool> AddSubscription(string username)
        {
            var user = _userService.GetUserByUsername(username);
            return user != null && await _subscriptionRepository.Add(user.Id);
        }

        public async Task<bool> DeleteSubscription(string username)
        {
            var user = _userService.GetUserByUsername(username);
            return user != null && await _subscriptionRepository.Delete(user.Id);
        }

        public Subscription GetSubscription(string username)
        {
            var user = _userService.GetUserByUsername(username);
            return user != null ? _subscriptionRepository.GetSubscription(user.Id) : null;
        }

        public async Task<List<Subscription>> GetSubscriptions()
        {
            return await _subscriptionRepository.GetSubscriptions();
        }
    }
}
