using Readible.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readible.Domain.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<List<Subscription>> GetSubscriptions();
        Subscription GetSubscription(int userId);
        Task<bool> Add(int userId);
        Task<bool> Delete(int userId);
    }
}
