﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Charge.Activity.Service.Repository.Entity {
    public interface IChargeActivityRepository {
        bool Add(string identifier);
    }
}
