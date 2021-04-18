using Readible.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readible.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        User GetUserById(int userId);
        User GetUserByUsername(string username);
        Task<bool> DeleteUser(int userId);
        Task<bool> AddUser(User user);
        Task<bool> UpdateUserPassword(string username, string userPassword);
    }
}
