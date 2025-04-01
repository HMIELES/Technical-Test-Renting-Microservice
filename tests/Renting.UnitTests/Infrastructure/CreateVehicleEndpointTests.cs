using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Renting.UnitTests.Infrastructure
{
    public class CreateVehicleEndpointTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CreateVehicleEndpointTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Should_Return_BadRequest_When_Model_Is_Invalid()
        {
            // Arrange - enviamos un JSON vacío (modelo inválido)
            var invalidRequest = new StringContent("{}", Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/vehicles", invalidRequest);

            // Assert - esperamos 400 BadRequest por validación de modelo
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
