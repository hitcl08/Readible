using Readible.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readible.Domain.Interfaces
{
    public interface ISubscriptionService
    {
        Task<List<Subscription>> GetSubscriptions();
        Subscription GetSubscription(string username);
        Task<bool> AddSubscription(string username);
        Task<bool> DeleteSubscription(string username);
    }
}
