﻿using Readible.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readible.Domain.Tests.MockData
{
    internal static class Users
    {
        internal static List<User> GetValidUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = 1,
                    Password = "hello",
                    SubscriptionId  = 1,
                    Username = "jonny"
                },
                new User
                {
                    Id = 2,
                    Password = "asdasd",
                    SubscriptionId  = 2,
                    Username = "jim"
                }
            };
        }

        internal static User GetValidUser()
        {
            return new User
            {
                Id = 1,
                Password = "hello",
                SubscriptionId = 1,
                Username = "jonny"
            };
        }

        internal static User GetInvalidUser()
        {
            return null;
        }
    }
}
