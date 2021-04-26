using Readible.Domain.Models;
using System.Collections.Generic;

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

        internal static Subscription GetValidSubscription()
        {
            return new Subscription
            {
                Id = 1,
                UserId = 3
            };
        }

        internal static Subscription GetInvalidSubscription()
        {
            return null;
        }
    }
}
