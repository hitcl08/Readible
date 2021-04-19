using Readible.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readible.Domain.Tests.MockData
{
    internal static class Subscriptions
    {
        internal static List<Subscription> GetSubscriptions()
        {
            return new List<Subscription>
            {
                new Subscription
                {
                    Id = 1,
                    UserId = 1
                },
                new Subscription
                {
                    Id = 2,
                    UserId = 1
                }
            };
        }

        internal static Subscription GetSubscription()
        {
            return new Subscription
            {
                Id = 1,
                UserId = 3
            };
        }
    }
}
