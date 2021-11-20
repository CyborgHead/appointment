using System;
using System.ComponentModel.DataAnnotations;

namespace appointment.core.DTOs
{
    public class AppointmentDto
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Date { get; set; }
    }
}
