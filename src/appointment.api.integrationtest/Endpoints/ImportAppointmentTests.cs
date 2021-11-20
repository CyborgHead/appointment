#region namespaces

using appointment.core.Contracts;
using appointment.core.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

#endregion namespaces

namespace appointment.api.integrationtest.IntegrationTests
{
    public class ImportAppointmentTests
        : IClassFixture<CustomApplicationFactory>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ImportAppointmentTests(CustomApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task WhenCreateAppointmentWithList_Then200()
        {
            /* Arrange - the server on which the API needs to be called on */

            // the factory instance of type WebApplicationFactory<Startup> 
            // returns a HttpClient for the test server.
            var client = _factory.CreateClient();

            List<AppointmentDto> itemList = new List<AppointmentDto>()
            {
                new AppointmentDto
                {
                    Name = "Agent 00",
                    Email = "zero@company.com",
                    PhoneNumber = "000000000",
                    StartTime = new DateTime(2021, 11, 20, 15, 00, 00),
                    EndTime = new DateTime(2021, 11, 20, 16, 00, 00),
                    Date = new DateTime(2021, 11, 20)
                },
                new AppointmentDto
                {
                    Name = "Agent 01",
                    Email = "zeroone@company.com",
                    PhoneNumber = "0000000001",
                    StartTime = new DateTime(2021, 11, 20, 15, 30, 00),
                    EndTime = new DateTime(2021, 11, 20, 16, 30, 00),
                    Date = new DateTime(2021, 11, 20)
                },
                new AppointmentDto
                {
                    Name = "Agent 02",
                    Email = "zerotwo@company.com",
                    PhoneNumber = "0000000002",
                    StartTime = new DateTime(2021, 11, 20, 16, 00, 00),
                    EndTime = new DateTime(2021, 11, 20, 17, 00, 00),
                    Date = new DateTime(2021, 11, 20)
                }
            };


            var httpContent = new StringContent(JsonConvert.SerializeObject(itemList), Encoding.UTF8, "application/json");

            /* Act - invoke the Create API */
            var response = await client.PostAsync("/api/v1/appointment/import", httpContent);

            /* Assert - the result is successful and the contents are non-empty */

            response.EnsureSuccessStatusCode();

            //Deserialize the content and check if the result is non-empty
            var responseContent = await response.Content.ReadAsStringAsync();
            var dtoItemList = JsonConvert.DeserializeObject<List<Response<AppointmentDto>>>(responseContent);

            Assert.Equal("200", dtoItemList[0].StatusCode);
        }
    }
}
