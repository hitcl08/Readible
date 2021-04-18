using Readible.Domain.Interfaces;
using Readible.Domain.Models;
using Readible.ServiceModel.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readible.Domain.Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<bool> AddUser(string username, string password)
        {
            var newUser = new User
            {
                Password = password,
                Username = username,
                SubscriptionId = null // new user begins without a subscription
            };

            return _userRepository.AddUser(newUser);
        }

        public Task<bool> DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public User GetUserByUsername(string username)
        {
            return _userRepository.GetUserByUsername(username);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public Task<bool> UpdateUserPassword(string username, string password)
        {
            return _userRepository.UpdateUserPassword(username, password);
        }
    }
}
