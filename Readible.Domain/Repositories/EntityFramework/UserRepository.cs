using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Readible.Domain.Interfaces;
using Readible.Domain.Models;
using Readible.Domain.Repositories.EntityFramework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readible.Domain.Repositories.EntityFramework
{
    public class UserRepository : IUserRepository
    {
        protected readonly ReadibleContext _context;
        private readonly IMapper _mapper;

        public UserRepository(ReadibleContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddUser(User newUser)
        {
            var isExistingUser = _context.User
                .Any(x => x.Username == newUser.Username); 

            if (isExistingUser)
            {
                return false;
            }

            var user = new UserViewModel 
            { 
                Username = newUser.Username, 
                Password = newUser.Password, 
                SubscriptionId = newUser.SubscriptionId 
            };

            var addedUser = _context.User.Add(user);
            await _context.SaveChangesAsync();

            return addedUser != null;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var userToDelete = _context.User.Where(x => x.Id == userId).FirstOrDefault();

            if (userToDelete == null)
            {
                return false;
            }
            var deletedUser = _context.User.Remove(userToDelete);
            await _context.SaveChangesAsync();

            return deletedUser != null;
        }

        public User GetUserById(int userId)
        {
            var userViewModel = _context.User.Where(x => x.Id == userId).FirstOrDefault();
            var selectedUser = _mapper.Map<UserViewModel, User>(userViewModel);
            return selectedUser;
        }

        public User GetUserByUsername(string username)
        {
            var userViewModel = _context.User.Where(x => x.Username == username).FirstOrDefault();
            var selectedUser = _mapper.Map<UserViewModel, User>(userViewModel);
            return selectedUser;
        }

        public async Task<List<User>> GetUsers()
        {
            var userViewModelList = await _context.User.ToListAsync();
            var userList = _mapper.Map<List<UserViewModel>, List<User>>(userViewModelList);
            return userList;
        }

        public async Task<bool> UpdateUserPassword(string username, string userPassword)
        {
            var existingUser = _context.User.Where(x => x.Username == username).FirstOrDefault();
            existingUser.Password = userPassword;
            await _context.SaveChangesAsync();

            var updatedUser = _context.User.Where(x => x.Username == username).FirstOrDefault();

            return updatedUser.Password == userPassword;
        }
    }
}
