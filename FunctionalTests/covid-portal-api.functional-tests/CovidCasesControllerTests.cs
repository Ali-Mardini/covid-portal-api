using covid_portal_api.api;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Threading.Tasks;
using Xunit;

namespace covid_portal_api.functional_tests
{
    public class CovidCasesControllerTests
        :IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public CovidCasesControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/v1/CovidCases/sync-data")]
        [InlineData("/api/v1/CovidCases/covid-summary")]
        [InlineData("/api/v1/CovidCases/history/united arab emirates?startDate=2021-09-01&endDate=2021-10-05")]
        public async Task Get_RequestForCovidCasesController(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}
