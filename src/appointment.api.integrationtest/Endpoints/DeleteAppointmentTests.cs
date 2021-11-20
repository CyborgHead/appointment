#region namespaces

using appointment.core.Contracts;
using appointment.core.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

#endregion namespaces

namespace appointment.api.integrationtest.IntegrationTests
{
    public class DeleteAppointmentTests
        : IClassFixture<CustomApplicationFactory>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public DeleteAppointmentTests(CustomApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task WhenDeleteAppointment_Then200()
        {
            /* Arrange - the server on which the API needs to be called on */

            // the factory instance of type WebApplicationFactory<Startup> 
            // returns a HttpClient for the test server.
            var client = _factory.CreateClient();

            /* Act - invoke the Retrieve API */
            var response = await client.GetAsync("/api/v1/appointment");

            //Checks if the response status code is OK or not.
            response.EnsureSuccessStatusCode();

            //Deserialize the content and check if the resultant list is non-empty
            var content = await response.Content.ReadAsStringAsync();

            var dtoList = JsonConvert.DeserializeObject<Response<List<AppointmentDto>>>(content);

            var item = dtoList.Data[0];

            var httpContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

            /* Act - invoke the Delete API */
            var deleteResponse = await client.DeleteAsync($"/api/v1/appointment/{item.Id}");

            /* Assert - the result is successful and the contents are non-empty */

            deleteResponse.EnsureSuccessStatusCode();

            var updateResponseContent = await deleteResponse.Content.ReadAsStringAsync();
            var dtoItem = JsonConvert.DeserializeObject<Response<string>>(updateResponseContent);

            Assert.Equal("200", dtoItem.StatusCode);
        }
    }
}
