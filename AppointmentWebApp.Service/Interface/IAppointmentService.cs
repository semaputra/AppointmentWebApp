using AppointmentWebApp.BussinessModel.ViewModel;
using AppointmentWebApp.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentWebApp.Service.Interface
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentViewModel>> GetAppointments();
        Task<AppointmentViewModel> GetAppointment(Guid id);
        Task<Guid> Add(AppointmentViewModel appointment);
        Task<AppointmentViewModel> Update(AppointmentViewModel appointment);
        Task<AppointmentViewModel> Delete(Guid id);
    }
}
