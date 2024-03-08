using AppointmentWebApp.BussinessModel.ViewModel;
using AppointmentWebApp.DataAccess.Models;
using AutoMapper;

namespace AppointmentWebApp.API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Muser,UserViewModel>().ReverseMap();
            CreateMap<Tappointment,AppointmentViewModel>().ReverseMap();
        }
    }
}
