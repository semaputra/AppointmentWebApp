using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentWebApp.BussinessModel.ViewModel
{
    public class AppointmentViewModel
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

        //Added Properties
        public string? LocationName { get; set; }
        public string? AppointmentDateStr { get; set; }
        public string? Username { get; set; }

    }
}
