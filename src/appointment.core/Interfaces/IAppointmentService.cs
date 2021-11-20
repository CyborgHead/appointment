using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using appointment.core.Contracts;
using appointment.core.DTOs;

namespace appointment.core.Interfaces
{
    public interface IAppointmentService
    {
        Task<Response<AppointmentDto>> CreateAppointment(AppointmentDto dto);
        Task<List<Response<AppointmentDto>>> CreateAppointmentsWithList(List<AppointmentDto> dtoList);
        Task<Response<AppointmentDto>> UpdateAppointment(AppointmentDto dto);
        Task<Response<string>> DeleteAppointment(Guid id);
        Task<Response<List<AppointmentDto>>> GetAllAppointments();
        Task<Response<AppointmentDto>> GetAppointmentById(Guid id);
    }
}
