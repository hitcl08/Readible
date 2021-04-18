using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Readible.Domain.Interfaces;
using Readible.Domain.Models;
using Readible.Domain.Repositories.EntityFramework;
using Readible.Domain.Repositories.EntityFramework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Readible.Domain.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly UserRepository _userRepository;

        public UserRepositoryTests()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ReadibleContext>();
            dbContextOptionsBuilder.UseSqlServer("Server=tcp:readible.database.windows.net,1433;Initial Catalog=readible-db;Persist Security Info=False;User ID=liamhitchcock;Password=4fBuJ976YAUdwRXQ;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
                .EnableSensitiveDataLogging();


            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserViewModel, User>();
            });

            var mapper = config.CreateMapper();


            var context = new ReadibleContext(dbContextOptionsBuilder.Options);
            _userRepository = new UserRepository(context, mapper);
        }

        [Fact]
        public async Task AddUser_ShouldReturnTrue_WhenUserIsAdded()
        {
            // arrange
            var newUser = MockData.Users.GetValidUser();

            // act
            var isUserAdded = await _userRepository.AddUser(newUser);

            // assert
            Assert.True(isUserAdded);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnTrue_WhenUserIsDeleted()
        {
            // arrange
            var userId = 1;
            
            // act
            var isUserAdded = await _userRepository.DeleteUser(userId);

            // assert
            Assert.True(isUserAdded);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnUser_ForValidUserId()
        {
            // arrange
            var userId = 2;

            // act
            var user = _userRepository.GetUserById(userId);

            // assert
            Assert.Equal(user.Id, userId);
        }


        [Fact]
        public async Task GetUserById_ShouldReturnNull_ForInvalidUserId()
        {
            // arrange
            var userId = 9099090;

            // act
            var user = _userRepository.GetUserById(userId);

            // assert
            Assert.Null(user);
        }

        [Fact]
        public async Task GetUsers_ShouldReturnListOfUsers_WhenValidRequest()
        {
            // arrange

            // act
            var userList = await _userRepository.GetUsers();

            // assert
            Assert.NotEmpty(userList);
        }

        [Fact]
        public async Task GetUsersByUsername_ShouldReturnUser_WhenValidUsername()
        {
            // arrange
            var username = "jonny";

            // act
            var user = _userRepository.GetUserByUsername(username);

            // assert
            Assert.Equal(user.Username, username);
        }

        [Fact]
        public async Task UpdateUserPassword_ShouldReturnTrue_WhenValidUsernamePassword()
        {
            // arrange
            var username = "jonny";
            var password = "this a new password";

            // act
            var request = await _userRepository.UpdateUserPassword(username, password);

            // assert
            Assert.True(request);
        }
    }
}
