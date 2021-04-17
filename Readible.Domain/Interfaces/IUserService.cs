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
        Task<User> GetUser(int id);
        Task<User> AddUser(string username, string password);
        Task<bool> DeleteUser(int id);
        Task<User> UpdateUserPassword(string username, string password);
    }
}
