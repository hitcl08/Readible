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

        public async Task<bool> AddUser(User user)
        {
            var newUser = new UserViewModel
            {
                Username = user.Username,
                Password = user.Password
            };

            var addedUser = _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return addedUser != null;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var userToDelete = _context.Users.Where(x => x.Id == userId).FirstOrDefault();

            var deletedUser = _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();

            return deletedUser != null;
        }

        public User GetUserById(int userId)
        {
            var userViewModel = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            return _mapper.Map<UserViewModel, User>(userViewModel);
        }

        public User GetUserByUsername(string username)
        {
            var userViewModel = _context.Users.Where(x => x.Username == username).FirstOrDefault();
            var selectedUser = _mapper.Map<UserViewModel, User>(userViewModel);
            return selectedUser;
        }

        public async Task<List<User>> GetUsers()
        {
            var userViewModelList = await _context.Users.ToListAsync();
            var userList = _mapper.Map<List<UserViewModel>, List<User>>(userViewModelList);
            return userList;
        }

        public async Task<bool> UpdateUserPassword(string username, string userPassword)
        {
            var existingUser = _context.Users.Where(x => x.Username == username).FirstOrDefault();
            if (existingUser == null)
            {
                return false;
            }
            existingUser.Password = userPassword;
            await _context.SaveChangesAsync();

            var updatedUser = _context.Users.Where(x => x.Username == username).FirstOrDefault();

            return updatedUser.Password == userPassword;
        }
    }
}
