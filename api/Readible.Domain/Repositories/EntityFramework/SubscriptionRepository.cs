using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Readible.Domain.Interfaces;
using Readible.Domain.Models;
using Readible.Domain.Repositories.EntityFramework.ViewModels;
using System.Collections.Generic;
using System.Linq;
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
            var addedSubscription = _context.Subscriptions
                .Add(new SubscriptionViewModel
                {
                    UserId = userId
                });

            await _context.SaveChangesAsync();
            return addedSubscription != null;
        }

        public async Task<bool> DeleteByUserId(int userId)
        {
            var subscriptionToDelete = _context.Subscriptions.Where(s => s.UserId == userId).FirstOrDefault();
            var subBooksToDelete = await _context.SubscriptionBooks.Where(sb => sb.SubscriptionId == subscriptionToDelete.Id).ToListAsync();

            var deletedSub = _context.Subscriptions.Remove(subscriptionToDelete);
            _context.SubscriptionBooks.RemoveRange(subBooksToDelete);
            await _context.SaveChangesAsync();

            return deletedSub != null;
        }

        public async Task<bool> DeleteBySubscriberId(int subscriptionId)
        {
            var subscriptionToDelete = _context.Subscriptions.Where(s => s.Id == subscriptionId).FirstOrDefault();
            var subBooksToDelete = await _context.SubscriptionBooks.Where(sb => sb.SubscriptionId == subscriptionToDelete.Id).ToListAsync();

            var deletedSub = _context.Subscriptions.Remove(subscriptionToDelete);
            _context.SubscriptionBooks.RemoveRange(subBooksToDelete);
            await _context.SaveChangesAsync();

            return deletedSub != null;
        }

        public Subscription GetSubscriptionBySubscriptionId(int subscriptionId)
        {
            var subscriptionViewModel = _context.SubscriptionBooks
                //.Where(x => x.SubscriptionId == subscriptionId)
                .Join(
                _context.Books,
                s => s.BookId,
                b => b.Id,
                (s, b) => new SubscriptionViewModel
                {
                    Id = s.SubscriptionId,
                    SubscriptionBooks = b.SubscriptionBooks.ToList(),
                })
                .FirstOrDefault();

            return _mapper.Map<SubscriptionViewModel, Subscription>(subscriptionViewModel);
        }

        public Subscription GetSubscriptionByUserId(int userId)
        {
            var subscriptionViewModel = _context.Subscriptions
                .Where(x => x.UserId == userId)
                .FirstOrDefault();

            return _mapper.Map<SubscriptionViewModel, Subscription>(subscriptionViewModel);
        }


        public async Task<List<Subscription>> GetSubscriptions()
        {
            var subscriptionViewModelList = await _context.Subscriptions.ToListAsync();
            return _mapper.Map<List<SubscriptionViewModel>, List<Subscription>>(subscriptionViewModelList);
        }
    }
}
