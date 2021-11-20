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
    public class RetrieveAppointmentTests
        : IClassFixture<CustomApplicationFactory>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public RetrieveAppointmentTests(CustomApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task WhenRetrieveAppointments_Then200()
        {
            /* Arrange - the server on which the API needs to be called on */

            // the factory instance of type WebApplicationFactory<Startup> 
            // returns a HttpClient for the test server.
            var client = _factory.CreateClient();

            /* Act - invoke the API */
            var response = await client.GetAsync("/api/v1/appointment");

            /* Assert - the result is successful and the contents are non-empty */

            //Checks if the response status code is OK or not.
            response.EnsureSuccessStatusCode();

            //Deserialize the content and check if the resultant list is non-empty
            var content = await response.Content.ReadAsStringAsync();

            var dtoList = JsonConvert.DeserializeObject<Response<List<AppointmentDto>>>(content);
            Assert.True(dtoList.Data.Count > 0);

            // Uncomment here for batch update
            //foreach (var item in dtoList.Data)
            //{
            //    item.IsPublished = true;

            //    var httpContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

            //    var response2 = await client.PutAsync("/api/v1/appointment", httpContent);

            //    string statusCode = null;
            //}


        }
    }
}
