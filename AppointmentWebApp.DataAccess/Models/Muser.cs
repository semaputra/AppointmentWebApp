using System;
using System.Collections.Generic;

namespace AppointmentWebApp.DataAccess.Models
{
    public partial class Muser
    {
        public Muser()
        {
            Tappointments = new HashSet<Tappointment>();
        }

        public Guid UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }

        public virtual ICollection<Tappointment> Tappointments { get; set; }
    }
}
