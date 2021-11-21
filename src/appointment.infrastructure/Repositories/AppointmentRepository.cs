#region namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using appointment.core.Interfaces;
using appointment.core.Models;

#endregion namespaces

namespace appointment.infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DynamoDBContext _context;

        public AppointmentRepository(IAmazonDynamoDB context)
        {
            _context = new DynamoDBContext(context);
        }

        public async Task SaveAppointment(AppointmentTable model) =>
            await _context.SaveAsync(model);

        public async Task DeleteAppointment(AppointmentTable model) =>
            await _context.DeleteAsync(model);

        public async Task<List<AppointmentTable>> GetAllAppointments() =>
            await _context.ScanAsync<AppointmentTable>(new List<ScanCondition>()).GetRemainingAsync();

        public async Task<AppointmentTable> GetAppointmentById(Guid id) =>
            await _context.LoadAsync<AppointmentTable>(id);

        public async Task<IEnumerable<AppointmentTable>> GetAppointmentsByEmail(string email)
        {
            return await _context.QueryAsync<AppointmentTable>(email,
                new DynamoDBOperationConfig { IndexName = "email-index" }).GetRemainingAsync();
        }

    }
}
