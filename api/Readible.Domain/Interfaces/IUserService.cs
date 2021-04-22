using Readible.Domain.Models;
using Readible.ServiceModel.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        Task<bool> UpdateUserPassword(string username, string password);
        User Authenticate(string username, string password);
    }
}
