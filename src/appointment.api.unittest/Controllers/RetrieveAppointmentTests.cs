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
using System.Collections.Generic;

#endregion namespaces

namespace appointment.api.unittest.Controllers
{
    public class RetrieveAppointmentTests
    {
        private readonly Mock<IAppointmentService> _appointmentServiceMock;
        private readonly Guid _Id = Guid.NewGuid();

        public RetrieveAppointmentTests()
        {
            _appointmentServiceMock = new Mock<IAppointmentService>(); ;
        }

        [Fact]
        public async void WhenGetAllAppointments_Then200()
        {
            // Arrange
            _appointmentServiceMock.Setup(p => p.GetAllAppointments()).ReturnsAsync(
                new Response<List<AppointmentDto>>()
                {
                    StatusCode = "200",
                    Data = new List<AppointmentDto>()
                    {
                        new AppointmentDto { Id = _Id }
                    }
                });
            AppointmentController appointmentController = new AppointmentController(_appointmentServiceMock.Object);

            // Act
            var response = await appointmentController.Get();

            //Assert
            Assert.NotNull(response);
            var returnValue = Assert.IsType<OkObjectResult>(response.Result);
            var appointmentResponse = Assert.IsType<Response<List<AppointmentDto>>>(returnValue.Value);
            Assert.Equal((int)HttpStatusCode.OK, returnValue.StatusCode);
            Assert.Equal(_Id, appointmentResponse.Data[0].Id);
        }

        [Fact]
        public async void WhenGetAppointmentById_Then200()
        {
            //Arrange
            _appointmentServiceMock.Setup(p => p.GetAppointmentById(_Id)).ReturnsAsync(
                new Response<AppointmentDto>()
                {
                    StatusCode = "200",
                    Data = new AppointmentDto() { Id = _Id }
                });
           

            AppointmentController appointmentController = new AppointmentController(_appointmentServiceMock.Object);

            //Act
            var response = await appointmentController.GetById(_Id);

            //Assert
            Assert.NotNull(response);
            var returnValue = Assert.IsType<OkObjectResult>(response.Result);
            var appointmentResponse = Assert.IsType<Response<AppointmentDto>>(returnValue.Value);
            Assert.Equal(appointmentResponse.Data.Id, _Id);

        }
    }
}
