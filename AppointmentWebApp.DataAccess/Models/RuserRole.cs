using System;
using System.Collections.Generic;

namespace AppointmentWebApp.DataAccess.Models
{
    public partial class RuserRole
    {
        public Guid UserId { get; set; }
        public int RoleId { get; set; }

        public virtual Mrole Role { get; set; } = null!;
        public virtual Muser User { get; set; } = null!;
    }
}
