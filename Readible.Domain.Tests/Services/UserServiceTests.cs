﻿using Moq;
using Readible.Domain.Interfaces;
using Readible.Domain.Models;
using Readible.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Readible.Domain.Tests.Services
{
    public class UserServiceTests
    {
        private readonly MockRepository MockRepository;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private IUserService _userService;
        public UserServiceTests()
        {
            MockRepository = new MockRepository(MockBehavior.Loose);
            _userRepositoryMock = MockRepository.Create<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetUsers_ShouldReturnListOfUsers_WhenServiceIsCalled()
        {
            // arrange
            _userRepositoryMock.Setup(x => x.GetUsers()).ReturnsAsync(MockData.Users.GetValidUsers());

            // act
            var result = await _userService.GetUsers();

            // assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetUser_ShouldReturnUserDetails_ForValidUserId()
        {
            // arrange
            var userId = 1;
            _userRepositoryMock.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(MockData.Users.GetValidUser());

            // act 
            var result = _userService.GetUserById(userId);

            // assert
            Assert.Equal(result.Id, userId);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnTrue_WhenUserIsDeleted()
        {
            var userId = 1;
            _userRepositoryMock.Setup(x => x.DeleteUser(It.IsAny<int>())).ReturnsAsync(true);

            // act 
            var result = await  _userService.DeleteUser(userId);

            // assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetUser_ShouldReturnNull_ForInvalidUserid()
        {
            var userId = -1;
            _userRepositoryMock.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(MockData.Users.GetInvalidUser());

            // act 
            var result =  _userService.GetUserById(userId);

            // assert
            Assert.Null(result);
        }

        [Fact]
        public async Task PostUser_ShouldReturnNewUser_WhenUserIsAdded()
        {
            _userRepositoryMock.Setup(x => x.AddUser(It.IsAny<User>())).ReturnsAsync(true);

            // act 
            var result = await _userService.AddUser("jonny", "hello");

            // assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnValidUserDetails_WhenUserPasswordUpdated()
        {
            //a rrange
            var userPassword = "hello";
            var username = "jonny";
            _userRepositoryMock.Setup(x => x.UpdateUserPassword(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            // act 
            var result = await _userService.UpdateUserPassword(username, userPassword);

            // assert
            Assert.True(result);
        }
    }
}
