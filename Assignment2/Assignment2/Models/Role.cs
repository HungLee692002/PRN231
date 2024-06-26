﻿using System;
using System.Collections.Generic;

namespace Assignment2.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string? RoleDesc { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
