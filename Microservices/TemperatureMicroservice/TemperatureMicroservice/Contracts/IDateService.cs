﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemperatureMicroservice.Contracts
{
    public interface IDateService
    {
        String ConvertDateToString(DateTime date);
    }
}
