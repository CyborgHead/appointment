#region namespaces

using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using appointment.core.Contracts;
using appointment.core.DTOs;
using appointment.core.Interfaces;
using System.Collections.Generic;

#endregion namespaces

namespace appointment.api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));
        }

        /// <summary>
        /// Create appointment.
        /// </summary>
        /// <param name="requestDto">AppointmentDto</param>
        /// <returns>AppointmentDto</returns>
        [HttpPost, Route("appointment")]
        public async Task<ActionResult<Response<string>>> Create([FromBody] AppointmentDto requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest(new Response<string>()
                {
                    StatusCode = "400",
                    StatusMessage = "Request can NOT be null or empty."
                });
            }
            try
            {
                var response = await _appointmentService.CreateAppointment(requestDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>()
                {
                    StatusCode = "400",
                    StatusMessage = ex.Message
                });
            }
        }

        /// <summary>
        /// Creates list of appointments with given input array.
        /// </summary>
        /// <param name="requestDtoList">List of AppointmentDto</param>
        /// <returns>AppointmentDtoList</returns>
        [HttpPost, Route("appointment/import")]
        public async Task<ActionResult<Response<string>>> CreateWithList([FromBody] List<AppointmentDto> requestDtoList)
        {
            if (requestDtoList == null || requestDtoList.Count == 0)
            {
                return BadRequest(new Response<string>()
                {
                    StatusCode = "400",
                    StatusMessage = "Request can NOT be null or empty."
                });
            }
            try
            {
                var response = await _appointmentService.CreateAppointmentsWithList(requestDtoList);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>()
                {
                    StatusCode = "400",
                    StatusMessage = ex.Message
                });
            }
        }

        /// <summary>
        /// Update appointment.
        /// </summary>
        /// <param name="requestDto">AppointmentDto</param>
        /// <returns>AppointmentDto</returns>
        [HttpPut, Route("appointment")]
        public async Task<ActionResult<Response<string>>> Update([FromBody] AppointmentDto requestDto)
        {
            if (requestDto == null || requestDto.Id.Equals(Guid.Empty))
            {
                return BadRequest(new Response<string>()
                {
                    StatusCode = "400",
                    StatusMessage = "Request data can NOT be null or empty."
                });
            }
            try
            {
                var response = await _appointmentService.UpdateAppointment(requestDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>()
                {
                    StatusCode = "400",
                    StatusMessage = ex.Message
                });
            }
        }

        /// <summary>
        /// Delete appointment.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns></returns>
        [HttpDelete, Route("appointment/{id}")]
        public async Task<ActionResult<Response<string>>> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new Response<string>()
                {
                    StatusCode = "400",
                    StatusMessage = "Appointment id can NOT be null or empty."
                });
            }
            try
            {
                var response = await _appointmentService.DeleteAppointment(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>()
                {
                    StatusCode = "400",
                    StatusMessage = ex.Message
                });
            }
        }

        /// <summary>
        /// Retrieve all appointments.
        /// </summary>
        /// <returns>ListOfAppointmentDTO</returns>
        [HttpGet, Route("appointment")]
        public async Task<ActionResult<Response<string>>> Get()
        {
            try
            {
                var response = await _appointmentService.GetAllAppointments();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>()
                {
                    StatusCode = "400",
                    StatusMessage = ex.Message
                });
            }
        }

        /// <summary>
        /// Retrieve appointment by id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>AppointmentDTO</returns>
        [HttpGet, Route("appointment/{id}")]
        public async Task<ActionResult<Response<string>>> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new Response<string>
                {
                    StatusCode = "400",
                    StatusMessage = "Appointment id can NOT be null or empty."
                });
            }
            try
            {
                var response = await _appointmentService.GetAppointmentById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>
                {
                    StatusCode = "400",
                    StatusMessage = ex.Message
                });
            }
        }

    }
}
