﻿using System;
using System.Collections.Generic;

namespace AppointmentWebApp.DataAccess.Models
{
    public partial class Mrole
    {
        public Mrole()
        {
            RuserRoles = new HashSet<RuserRole>();
        }

        public int RoleId { get; set; }
        public string? RoleName { get; set; }

        public virtual ICollection<RuserRole> RuserRoles { get; set; }
    }
}
