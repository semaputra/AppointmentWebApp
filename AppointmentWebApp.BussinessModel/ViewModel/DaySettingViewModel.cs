using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentWebApp.BussinessModel.ViewModel
{
    public class DaySettingViewModel
    {
        public long DaySettingId { get; set; }
        public DateTime? Day { get; set; }
        public long? Quantity { get; set; }
        public bool? DayStatus { get; set; }
    }
}
