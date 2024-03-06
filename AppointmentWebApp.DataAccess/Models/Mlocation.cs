using System;
using System.Collections.Generic;

namespace AppointmentWebApp.DataAccess.Models
{
    public partial class Mlocation
    {
        public Mlocation()
        {
            Tappointments = new HashSet<Tappointment>();
        }

        public int LocationId { get; set; }
        public string? LocationName { get; set; }

        public virtual ICollection<Tappointment> Tappointments { get; set; }
    }
}
