using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Readible.Domain.Interfaces;
using Readible.Domain.Models;
using Readible.Domain.Repositories.EntityFramework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readible.Domain.Repositories.EntityFramework
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly IMapper _mapper;
        protected readonly ReadibleContext _context;

        public SubscriptionRepository(ReadibleContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Add(int userId)
        {
            var user = _context.User.Where(x => x.Id == userId).FirstOrDefault();
            if (user == null)
            {
                return false;
            }

            // dont add new subscription if subscription already exists for user
            if (user.SubscriptionId > 0)
            {
                return false;
            }

            var addedSubscription = _context.Subscription
                .Add(new SubscriptionViewModel
                {
                    UserId = user.Id
                });

            await _context.SaveChangesAsync();
            await SetUserSubscriptionId(user, addedSubscription.Entity.Id);

            return addedSubscription != null;
        }

        public async Task<bool> Delete(int userId)
        {
            var user = _context.User.Where(x => x.Id == userId).FirstOrDefault();
            if (user == null)
            {
                return false;
            }

            var subscriptionId = _context.User
                .Where(x => x.SubscriptionId == user.SubscriptionId)
                .FirstOrDefault()
                .SubscriptionId;

            var subToDelete = _context.Subscription.Where(x => x.Id == subscriptionId).FirstOrDefault();
            var deletedSub = subToDelete != null ? _context.Subscription.Remove(subToDelete) : null;
            await _context.SaveChangesAsync();

            await SetUserSubscriptionId(user, -1);

            return deletedSub != null;
        }

        public Subscription GetSubscription(int userId)
        {
            var user = _context.User.Where(x => x.Id == userId).FirstOrDefault();
            if (user == null)
            {
                return null;
            }

            var subscriptionViewModel = _context.Subscription
                .Where(x => x.Id == user.SubscriptionId)
                .FirstOrDefault();

            return _mapper.Map<SubscriptionViewModel, Subscription>(subscriptionViewModel);
        }

        public async Task<List<Subscription>> GetSubscriptions()
        {
            var subscriptionViewModelList = await _context.Subscription.ToListAsync();
            return _mapper.Map<List<SubscriptionViewModel>, List<Subscription>>(subscriptionViewModelList);
        }

        private async Task SetUserSubscriptionId(UserViewModel user, int subscriptionId)
        {
            user.SubscriptionId = subscriptionId;
            await _context.SaveChangesAsync();
        }
    }
}
