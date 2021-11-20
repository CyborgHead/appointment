using System;
using Amazon.DynamoDBv2.DataModel;

namespace appointment.core.Models
{
    [DynamoDBTable("appointment-state-store")]
    public class AppointmentTable
    {
        [DynamoDBHashKey] //Partition key
        public Guid Id { get; set; }
        public string Name { get; set; }
        [DynamoDBGlobalSecondaryIndexHashKey("email-index")] // Hash key
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [DynamoDBGlobalSecondaryIndexRangeKey("email-index")] //Range key
        public DateTime Date { get; set; } // Create Date
        public DateTime LastModified { get; set; }
    }
}
