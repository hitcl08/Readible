using Moq;
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
            _userRepositoryMock.Setup(x => x.GetUser(userId)).ReturnsAsync(MockData.Users.GetValidUser());

            // act 
            var result = await _userService.GetUser(userId);

            // assert
            Assert.Equal(result.Id, userId);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnTrue_WhenUserIsDeleted()
        {
            var userId = 1;
            _userRepositoryMock.Setup(x => x.DeleteUser(userId)).ReturnsAsync(true);

            // act 
            var result = await  _userService.DeleteUser(userId);

            // assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetUser_ShouldReturnNull_ForInvalidUserid()
        {
            var userId = -1;
            _userRepositoryMock.Setup(x => x.GetUser(userId)).ReturnsAsync(MockData.Users.GetInvalidUser());

            // act 
            var result = await  _userService.GetUser(userId);

            // assert
            Assert.Null(result);
        }

        [Fact]
        public async Task PostUser_ShouldReturnNewUser_WhenUserIsAdded()
        {
            var user = new User();
            _userRepositoryMock.Setup(x => x.AddUser(user)).ReturnsAsync(new User());

            // act 
            var result = await _userService.AddUser("jonny", "hello");

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnValidUserDetails_WhenUserPasswordUpdated()
        {
            var userPassword = "hello";
            var username = "jonny";
            _userRepositoryMock.Setup(x => x.UpdateUserPassword(username, userPassword)).ReturnsAsync(MockData.Users.GetValidUser());

            // act 
            var result = await _userService.UpdateUserPassword(username, userPassword);

            // assert
            Assert.NotNull(result);
            Assert.Equal(userPassword, result.Password);
        }

    }
}
