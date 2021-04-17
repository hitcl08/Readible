using Readible.Domain.Interfaces;
using Readible.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readible.Domain.Repositories.EntityFramework
{
    public class UserRepository : IUserRepository
    {
        public Task<User> AddUser(User user)
        {

        }

        public Task<bool> DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUserPassword(string username, string userPassword)
        {
            throw new NotImplementedException();
        }
    }
}
