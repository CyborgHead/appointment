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
    public class UpdateAppointmentTests
    {
        private readonly Mock<IAppointmentService> _appointmentServiceMock;
        private readonly Guid _Id = Guid.NewGuid();

        public UpdateAppointmentTests()
        {
            _appointmentServiceMock = new Mock<IAppointmentService>(); ;
        }

        [Fact]
        public async void WhenUpdateAppointment_Then200()
        {
            // Arrange
            _appointmentServiceMock.Setup(p => p.UpdateAppointment(It.IsAny<AppointmentDto>())).ReturnsAsync(
                new Response<AppointmentDto>()
                {
                    StatusCode = "200",
                    Data = new AppointmentDto()
                    {
                        Id = _Id
                    }
                });
            AppointmentController appointmentController = new AppointmentController(_appointmentServiceMock.Object);

            // Act
            var response = await appointmentController.Update(new AppointmentDto() { Id = Guid.NewGuid() });

            //Assert
            Assert.NotNull(response);
            var returnValue = Assert.IsType<OkObjectResult>(response.Result);
            var AppointmentResponse = Assert.IsType<Response<AppointmentDto>>(returnValue.Value);
            Assert.Equal((int)HttpStatusCode.OK, returnValue.StatusCode);
            Assert.Equal(_Id, AppointmentResponse.Data.Id);
        }

        [Fact]
        public async void WhenUpdateAppointment_EmptyAppointmentId_Then400()
        {
            // Arrange
            _appointmentServiceMock.Setup(p => p.UpdateAppointment(It.IsAny<AppointmentDto>()))
                .ReturnsAsync(
                new Response<AppointmentDto>()
                {
                    StatusCode = "400"
                });
            AppointmentController appointmentController = new AppointmentController(_appointmentServiceMock.Object);

            // Act
            var response = await appointmentController.Update(null);

            //Assert
            Assert.NotNull(response);
            var returnValue = Assert.IsType<BadRequestObjectResult>(response.Result);
            var AppointmentResponse = Assert.IsType<Response<string>>(returnValue.Value);
            Assert.Equal((int)HttpStatusCode.BadRequest, returnValue.StatusCode);
            Assert.Contains("can NOT be null or empty.", AppointmentResponse.StatusMessage);
        }
    }
}
