using EmployeesApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeesApiIntegrationTests
{
    public class PlacingAnOrder : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public PlacingAnOrder(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task PlacingAnOrderBeforeCutoffShipsToday()
        {
            //before the cutoff
            _factory.SystemTimeToUse = new DateTime(2020, 7, 7);

            // i place an order
            var response = await _client.PostAsJsonAsync("/orders", new OrderToSend());

            // it ships same day
            var context = await response.Content.ReadAsAsync<OrderConfirmation>();
            Assert.Equal(7, context.estimatedShipDate.Date.Day);
        }
        public class OrderToSend
        {

        }

        public class OrderConfirmation
        {
            public DateTime estimatedShipDate { get; set; }
        }

    }
}
