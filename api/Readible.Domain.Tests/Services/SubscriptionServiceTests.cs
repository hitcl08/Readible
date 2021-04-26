using Moq;
using Readible.Domain.Interfaces;
using Readible.Domain.Services;
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
            var result = await _subscriptionService.GetAllSubscriptions();

            // assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetSubscription_ReturnsSubscriptionForUser_WhenValidUserId()
        {
            // arrange
            _subscriptionRepositoryMock.Setup(x => x.GetSubscriptionBySubscriptionId(It.IsAny<int>())).Returns(MockData.Subscriptions.GetValidSubscription());
            _userServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(MockData.Users.GetValidUser());

            // act
            var result = _subscriptionService.AddUserSubscription(1);

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddUserSubscription_ReturnsTrue_WhenValidUserId()
        {
            // arrange
            _subscriptionRepositoryMock.Setup(x => x.Add(It.IsAny<int>())).ReturnsAsync(true);
            _userServiceMock.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(MockData.Users.GetValidUser());

            // act
            var result = await _subscriptionService.AddUserSubscription(1);

            // assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddUserSubscription_ReturnsFalse_WhenInvalidUserId()
        {
            // arrange
            _subscriptionRepositoryMock.Setup(x => x.Add(It.IsAny<int>())).ReturnsAsync(false);
            _userServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(MockData.Users.GetInvalidUser());

            // act
            var result = await _subscriptionService.AddUserSubscription(-1);

            // assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteUserSubscription_ReturnsTrue_WhenValidUsername()
        {
            // arrange
            _subscriptionRepositoryMock.Setup(x => x.DeleteByUserId(It.IsAny<int>())).ReturnsAsync(true);
            _userServiceMock.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(MockData.Users.GetValidUser());

            // act
            var result = await _subscriptionService.DeleteUserSubscription(1);

            // assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteUserSubscription_ReturnsFalse_WhenInvalidUsername()
        {
            // arrange
            _subscriptionRepositoryMock.Setup(x => x.DeleteByUserId(It.IsAny<int>())).ReturnsAsync(false);
            _userServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(MockData.Users.GetInvalidUser());

            // act
            var result = await _subscriptionService.DeleteUserSubscription(-1);

            // assert
            Assert.False(result);
        }
    }
}
