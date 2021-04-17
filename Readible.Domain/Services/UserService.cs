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

        public Task<User> AddUser(string username, string password)
        {
            var newUser = new User
            {
                Id = new Random().Next(1000), // TODO: Investigate better way at generating unique id
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

        public Task<User> GetUser(int id)
        {
            return _userRepository.GetUser(id);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public Task<User> UpdateUserPassword(string username, string password)
        {
            return _userRepository.UpdateUserPassword(username, password);
        }
    }
}
