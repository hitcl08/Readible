using Readible.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Readible.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static User WithoutPassword(this User user)
        {
            if (user != null)
            {
                user.Password = null;
            }
            return user;
        }
    }
}
