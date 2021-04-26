using Readible.Domain.Interfaces;
using Readible.Domain.Models;
using Readible.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;


namespace Readible.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> AddUser(string username, string password)
        {
            if (username == null || password == null)
            {
                return false;
            }

            var newUser = new User
            {
                Password = BC.HashPassword(password),
                Username = username,
                Subscription = new Subscription
                {
                    Books = new List<Book>(),
                }
                // new user begins without a subscription
            };

            return await _userRepository.AddUser(newUser);
        }

        public User Authenticate(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);

            var verified = BC.Verify(password, user.Password);

            // return null if user not found or password incorrect
            if (!verified)
                return null;

            // authentication successful so return user details without password
            return user.WithoutPassword();
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await _userRepository.DeleteUser(id);
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public User GetUserByUsername(string username)
        {
            var user = _userRepository.GetUserByUsername(username);
            return user.WithoutPassword();
        }

        public async Task<List<User>> GetUsers()
        {

            return await _userRepository.GetUsers();
        }

        public async Task<bool> UpdateUserPassword(int userId, string password)
        {
            var encryptedPword = BC.HashPassword(password);
            return await _userRepository.UpdateUserPassword(userId, encryptedPword);
        }
    }
}
