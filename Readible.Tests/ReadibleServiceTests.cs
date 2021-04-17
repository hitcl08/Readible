using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;
using Readible.ServiceInterface.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;

namespace Readible.Tests
{
    public class ReadibleServiceTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private const string WeatherForecastUriPath = "weatherforecast";

        private static Mock<IReadibleService> _mockReadibleService;
        private readonly WebApplicationFactory<Startup> _webApplicationFactory;
        private static HttpClient _httpClient;

        public ReadibleServiceTests(WebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
            _httpClient ??= webApplicationFactory.CreateClient();
        }

        [Fact]
        public async void Get_Weather_Service_Test()
        {
            var weatherForecastResponse = await _httpClient.GetAsync(WeatherForecastUriPath);
            Assert.Equal(HttpStatusCode.OK, weatherForecastResponse.StatusCode);

            var weatherForecastContent = await weatherForecastResponse.Content.ReadAsStringAsync();
            var weatherForecast = JsonConvert.DeserializeObject<List<WeatherForecast>>(weatherForecastContent);

            Assert.NotNull(weatherForecast);
            Assert.NotNull(weatherForecast[0].Summary);
        }
    }
}
