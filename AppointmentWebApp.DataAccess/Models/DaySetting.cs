using System;
using System.Collections.Generic;

namespace AppointmentWebApp.DataAccess.Models
{
    public partial class DaySetting
    {
        public long DaySettingId { get; set; }
        public DateTime? Day { get; set; }
        public long? Quantity { get; set; }
        public bool? DayStatus { get; set; }
    }
}
