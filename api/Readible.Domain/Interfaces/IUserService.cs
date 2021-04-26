using Readible.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readible.Domain.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        User GetUserById(int id);
        User GetUserByUsername(string username);

        Task<bool> AddUser(string username, string password);
        Task<bool> DeleteUser(int id);
        Task<bool> UpdateUserPassword(int userId, string password);
        User Authenticate(string username, string password);
    }
}
