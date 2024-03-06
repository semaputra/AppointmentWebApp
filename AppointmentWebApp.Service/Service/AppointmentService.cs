using AppointmentWebApp.DataAccess.Models;
using AppointmentWebApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentWebApp.Service.Service
{
    public class AppointmentService : IAppointmentService
    {
        public Guid Add(Tappointment appointment)
        {
            throw new NotImplementedException();
        }

        public Tappointment GetAppointment(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tappointment> GetAppointments()
        {
            throw new NotImplementedException();
        }

        public void Update(Tappointment appointment)
        {
            throw new NotImplementedException();
        }
    }
}
