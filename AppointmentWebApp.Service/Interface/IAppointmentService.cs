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
        IEnumerable<Tappointment> GetAppointments();
        Tappointment GetAppointment(Guid id);
        Guid Add(Tappointment appointment);
        void Update(Tappointment appointment);
    }
}
