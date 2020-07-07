using EmployeesApi;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeesApiIntegrationTests
{
    public class ApiSmokeTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ApiSmokeTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/books")]
        [InlineData("/whoami")]
        [InlineData("/time")]
        public async Task ResourcesAreAlive(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
