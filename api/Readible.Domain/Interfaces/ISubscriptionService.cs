using Readible.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readible.Domain.Interfaces
{
    public interface ISubscriptionService
    {
        Task<List<Subscription>> GetAllSubscriptions();
        Subscription GetUserSubscription(int userId);
        Task<bool> AddUserSubscription(int userId);
        Task<bool> DeleteUserSubscription(int userId);
        Task<bool> DeleteSubscription(int subscriptionId);
    }
}
