using Readible.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readible.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(int userId);
        Task<bool> DeleteUser(int userId);
        Task<User> AddUser(User user);
        Task<User> UpdateUserPassword(string username, string userPassword);
    }
}
