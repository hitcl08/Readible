using Readible.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readible.Domain.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<List<Subscription>> GetSubscriptions();
        Subscription GetSubscriptionBySubscriptionId(int subscriptionId);
        Subscription GetSubscriptionByUserId(int userId);
        Task<bool> Add(int userId);
        Task<bool> DeleteByUserId(int userId);
        Task<bool> DeleteBySubscriberId(int subscriptionId);
    }
}
