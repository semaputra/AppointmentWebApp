using System;
using System.Collections.Generic;

namespace AppointmentWebApp.DataAccess.Models
{
    public partial class Tappointment
    {
        public Guid AppointmentId { get; set; }
        public Guid? UserId { get; set; }
        public string? AppointmentTitle { get; set; }
        public string? AppointmentPurpose { get; set; }
        public int? LocationId { get; set; }
        public string? LocationDescription { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }

        public virtual Mlocation? Location { get; set; }
        public virtual Muser? User { get; set; }
    }
}
