using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using appointment.core.Models;

namespace appointment.core.Interfaces
{
    public interface IAppointmentRepository
    {
        public Task SaveAppointment(AppointmentTable model);
        public Task DeleteAppointment(AppointmentTable model);
        public Task<List<AppointmentTable>> GetAllAppointments();
        public Task<AppointmentTable> GetAppointmentById(Guid id);
        public Task<IEnumerable<AppointmentTable>> GetAppointmentsByEmail(string email);
    }
}
