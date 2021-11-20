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
    public class CreateAppointmentTests
    {
        private readonly Mock<IAppointmentService> _AppointmentServiceMock;
        private readonly Guid _Id = Guid.NewGuid();

        public CreateAppointmentTests()
        {
            _AppointmentServiceMock = new Mock<IAppointmentService>(); ;
        }

        [Fact]
        public async void WhenCreateAppointment_Then200()
        {
            // Arrange
            _AppointmentServiceMock.Setup(p => p.CreateAppointment(It.IsAny<AppointmentDto>())).ReturnsAsync(
                new Response<AppointmentDto>()
                {
                    StatusCode = "200",
                    Data = new AppointmentDto()
                    {
                        Id = _Id
                    }
                });
            AppointmentController AppointmentController = new AppointmentController(_AppointmentServiceMock.Object);

            // Act
            var response = await AppointmentController.Create(new AppointmentDto() { Id = Guid.NewGuid() });

            //Assert
            Assert.NotNull(response);
            var returnValue = Assert.IsType<OkObjectResult>(response.Result);
            var AppointmentResponse = Assert.IsType<Response<AppointmentDto>>(returnValue.Value);
            Assert.Equal((int)HttpStatusCode.OK, returnValue.StatusCode);
            Assert.Equal(_Id, AppointmentResponse.Data.Id);
        }

        [Fact]
        public async void WhenCreateAppointment_EmptyAppointmentId_Then400()
        {
            // Arrange
            _AppointmentServiceMock.Setup(p => p.CreateAppointment(It.IsAny<AppointmentDto>()))
                .ReturnsAsync(
                new Response<AppointmentDto>()
                {
                    StatusCode = "400"
                });
            AppointmentController AppointmentController = new AppointmentController(_AppointmentServiceMock.Object);

            // Act
            var response = await AppointmentController.Create(null);

            //Assert
            Assert.NotNull(response);
            var returnValue = Assert.IsType<BadRequestObjectResult>(response.Result);
            var AppointmentResponse = Assert.IsType<Response<string>>(returnValue.Value);
            Assert.Equal((int)HttpStatusCode.BadRequest, returnValue.StatusCode);
            Assert.Contains("can NOT be null or empty.", AppointmentResponse.StatusMessage);
        }
    }
}
