﻿using System;
using System.Collections.Generic;

namespace Charge.Repository.Service.Repository.Entity.Models
{
    public partial class Activities
    {
        public int IdActivity { get; set; }
        public string Identifier { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
