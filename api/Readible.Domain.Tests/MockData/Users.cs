using Readible.Domain.Models;
using System.Collections.Generic;

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
                    Subscription  = new Subscription(),
                    Username = "jonny"
                },
                new User
                {
                    Id = 2,
                    Password = "asdasd",
                    Subscription  = new Subscription(),
                    Username = "jim"
                }
            };
        }

        internal static User GetValidUser()
        {
            return new User
            {
                Password = "hellos",
                Subscription = new Subscription(),
                Username = "jim"
            };
        }

        internal static User GetInvalidUser()
        {
            return null;
        }
    }
}
