using Moq;
using Readible.Domain.Interfaces;
using Readible.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Readible.Domain.Tests.Services
{
    public class SubscriptionServiceTests
    {
        private readonly MockRepository MockRepository;
        private readonly Mock<ISubscriptionRepository> _subscriptionRepositoryMock;
        private readonly ISubscriptionService _subscriptionService;
        private readonly Mock<IUserService> _userServiceMock;


        public SubscriptionServiceTests()
        {
            MockRepository = new MockRepository(MockBehavior.Loose);
            _subscriptionRepositoryMock = MockRepository.Create<ISubscriptionRepository>();
            _userServiceMock = MockRepository.Create<IUserService>();
            _subscriptionService = new SubscriptionService(_subscriptionRepositoryMock.Object, _userServiceMock.Object);
        }

        [Fact]
        public async Task GetSubscriptions_ReturnsListOfAllSubscriptions_WhenValidRequest()
        {
            // arrange
            _subscriptionRepositoryMock.Setup(x => x.GetSubscriptions()).ReturnsAsync(MockData.Subscriptions.GetSubscriptions());

            // act
            var result = await _subscriptionService.GetSubscriptions();

            // assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetSubscription_ReturnsSubscriptionForUser_WhenValidUserId()
        {
            // arrange
            var username = "asd";
            _subscriptionRepositoryMock.Setup(x => x.GetSubscription(It.IsAny<int>())).Returns(MockData.Subscriptions.GetSubscription());
            _userServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(MockData.Users.GetValidUser());

            // act
            var result = _subscriptionService.GetSubscription(username);

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddSubscription_ReturnsTrue_WhenValidUsername()
        {
            // arrange
            var username = "asd";
            _subscriptionRepositoryMock.Setup(x => x.Add(It.IsAny<int>())).ReturnsAsync(true);
            _userServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(MockData.Users.GetValidUser());

            // act
            var result = await _subscriptionService.AddSubscription(username);

            // assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddSubscription_ReturnsFalse_WhenInvalidUsername()
        {
            // arrange
            var username = "";
            _subscriptionRepositoryMock.Setup(x => x.Add(It.IsAny<int>())).ReturnsAsync(false);
            _userServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(MockData.Users.GetInvalidUser());

            // act
            var result = await _subscriptionService.AddSubscription(username);

            // assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteSubscription_ReturnsTrue_WhenValidUsername()
        {
            // arrange
            var username = "asd";
            _subscriptionRepositoryMock.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(true);
            _userServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(MockData.Users.GetValidUser());

            // act
            var result = await _subscriptionService.DeleteSubscription(username);

            // assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteSubscription_ReturnsFalse_WhenInvalidUsername()
        {
            // arrange
            var username = "";
            _subscriptionRepositoryMock.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(false);
            _userServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(MockData.Users.GetInvalidUser());

            // act
            var result = await _subscriptionService.AddSubscription(username);

            // assert
            Assert.False(result);
        }
    }
}
