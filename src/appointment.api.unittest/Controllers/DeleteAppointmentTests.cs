#region namespaces

using appointment.core.Interfaces;
using Moq;
using System;
using System.Net;
using appointment.core.DTOs;
using Xunit;
using appointment.api.Controllers;
using appointment.core.Contracts;
using appointment.core.Models;
using Microsoft.AspNetCore.Mvc;

#endregion namespaces

namespace appointment.api.unittest.Controllers
{
    public class DeleteAppointmentTests
    {
        private readonly Mock<IAppointmentService> _appointmentServiceMock;
        private readonly Guid _Id = Guid.NewGuid();

        public DeleteAppointmentTests()
        {
            _appointmentServiceMock = new Mock<IAppointmentService>(); ;
        }

        [Fact]
        public async void WhenDeleteAppointment_Then200()
        {
            // Arrange
            _appointmentServiceMock.Setup(p => p.DeleteAppointment(_Id)).ReturnsAsync(
                new Response<string>()
                {
                    StatusCode = "200",
                    Data = null
                });
            AppointmentController appointmentController = new AppointmentController(_appointmentServiceMock.Object);

            // Act
            var response = await appointmentController.Delete(_Id);

            //Assert
            Assert.NotNull(response);
            var returnValue = Assert.IsType<OkObjectResult>(response.Result);
            var AppointmentResponse = Assert.IsType<Response<string>>(returnValue.Value);
            Assert.Equal((int)HttpStatusCode.OK, returnValue.StatusCode);
        }

        [Fact]
        public async void WhenDeleteAppointment_EmptyAppointmentId_Then400()
        {
            // Arrange
            _appointmentServiceMock.Setup(p => p.DeleteAppointment(Guid.Empty))
                .ReturnsAsync(
                new Response<string>()
                {
                    StatusCode = "400"
                });
            AppointmentController appointmentController = new AppointmentController(_appointmentServiceMock.Object);

            // Act
            var response = await appointmentController.Delete(Guid.Empty);

            //Assert
            Assert.NotNull(response);
            var returnValue = Assert.IsType<BadRequestObjectResult>(response.Result);
            var AppointmentResponse = Assert.IsType<Response<string>>(returnValue.Value);
            Assert.Equal((int)HttpStatusCode.BadRequest, returnValue.StatusCode);
            Assert.Contains("can NOT be null or empty.", AppointmentResponse.StatusMessage);
        }
    }
}
