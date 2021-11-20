using AutoMapper;
using appointment.core.DTOs;
using appointment.core.Models;

namespace appointment.core.Profiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<AppointmentDto, AppointmentTable>();

            CreateMap<AppointmentTable, AppointmentDto>();
        }
    }
}
