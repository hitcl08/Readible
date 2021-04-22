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

        public async Task<bool> AddUserSubscription(int userId)
        {
            var user = _userService.GetUserById(userId);
            return user != null && await _subscriptionRepository.Add(user.Id);
        }

        public async Task<bool> DeleteUserSubscription(int userId)
        {
            var user = _userService.GetUserById(userId);
            return user != null && await _subscriptionRepository.DeleteByUserId(user.Id);
        }

        public async Task<bool> DeleteSubscription(int subscriptionId)
        {
            var subscription = _subscriptionRepository.GetSubscription(subscriptionId);
            return subscription != null && await _subscriptionRepository.DeleteBySubscriberId(subscription.Id);
        }

        public Subscription GetUserSubscription(int userId)
        {
            var user = _userService.GetUserById(userId);
            return user != null ? _subscriptionRepository.GetSubscription(user.Id) : null;
        }

        public async Task<List<Subscription>> GetAllSubscriptions()
        {
            return await _subscriptionRepository.GetSubscriptions();
        }
    }
}
