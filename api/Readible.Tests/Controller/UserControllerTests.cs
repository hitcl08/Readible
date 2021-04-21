using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;
using Readible.Domain.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;


namespace Readible.Tests.Controller
{
    public class UserControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private const string UserUri = "users";

        private Mock<IUserService> _mockUserService;


        private readonly WebApplicationFactory<Startup> _webApplicationFactory;
        private static HttpClient _httpClient;

        public UserControllerTests(WebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
            _httpClient ??= webApplicationFactory.CreateClient();

            _mockUserService = new Mock<IUserService>();
        }

        [Fact]
        public async Task Get_ShouldReturnListOfUsers_WhenRequestIsMade()
        {
            // Arrange

            // Act
            var usersResponse = await _httpClient.GetAsync(UserUri);

            // Assert
            var usersResponseContent = await usersResponse.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<int>>(usersResponseContent);

            Assert.Equal(HttpStatusCode.OK, usersResponse.StatusCode);
            Assert.Equal(2, users.Count);
        }

        [Fact]
        public async Task Get_ShouldReturnUserDetails_WhenUserIdIsPassedIn()
        {
            // Arrange
            var userId = 1;
            // Act
            var usersResponse = await _httpClient.GetAsync($"{UserUri}/${userId}");

            // Assert
            var usersResponseContent = await usersResponse.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<int>>(usersResponseContent);

            Assert.Equal(HttpStatusCode.OK, usersResponse.StatusCode);
            Assert.Equal(1, users.Count);
        }

        [Fact]
        public async Task Get_ShouldReturnNotFoundException_WhenUserIdDoesNotExist()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
