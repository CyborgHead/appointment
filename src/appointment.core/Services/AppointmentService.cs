#region namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using appointment.core.Contracts;
using appointment.core.DTOs;
using appointment.core.Interfaces;
using appointment.core.Models;

#endregion namespaces

namespace appointment.core.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response<AppointmentDto>> CreateAppointment(AppointmentDto appointmentDto)
        {
            appointmentDto.Id = Guid.NewGuid();
            var appointmentModel = _mapper.Map<AppointmentTable>(appointmentDto);
            appointmentModel.Date = DateTime.Now;
            appointmentModel.LastModified = DateTime.Now;

            await _appointmentRepository.SaveAppointment(appointmentModel);
            return new Response<AppointmentDto>()
            {
                StatusCode = "200",
                StatusMessage = $"Appointment created successfully with id {appointmentDto.Id}",
                Data = appointmentDto
            };
        }

        public async Task<Response<AppointmentDto>> UpdateAppointment(AppointmentDto appointmentDto)
        {
            var appointmentModel = _mapper.Map<AppointmentTable>(appointmentDto);
            appointmentModel.LastModified = DateTime.Now;
            await _appointmentRepository.SaveAppointment(appointmentModel);

            return new Response<AppointmentDto>()
            {
                StatusCode = "200",
                StatusMessage = $"Appointment updated successfully for id {appointmentModel.Id}",
                Data = appointmentDto
            };
        }

        public async Task<Response<string>> DeleteAppointment(Guid id)
        {
            await _appointmentRepository.DeleteAppointment(new AppointmentTable() { Id = id });

            return new Response<string>()
            {
                StatusCode = "200",
                StatusMessage = $"Appointment deleted successfully for id {id}",
                Data = null
            };
        }

        public async Task<Response<List<AppointmentDto>>> GetAllAppointments()
        {
            var appointmentItems = await _appointmentRepository.GetAllAppointments();
            return new Response<List<AppointmentDto>>()
            {
                StatusCode = "200",
                StatusMessage = $"Found {appointmentItems.Count} appointment",
                Data = appointmentItems.Select(n => _mapper.Map<AppointmentDto>(n)).ToList()
            };
        }

        public async Task<Response<AppointmentDto>> GetAppointmentById(Guid id)
        {
            var appointmentItem = await _appointmentRepository.GetAppointmentById(id);
            return new Response<AppointmentDto>()
            {
                StatusCode = "200",
                StatusMessage = $"Found appointment with Id : {id}",
                Data = _mapper.Map<AppointmentDto>(appointmentItem)
            };
        }

    }
}
