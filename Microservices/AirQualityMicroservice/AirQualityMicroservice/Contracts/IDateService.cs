using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirQualityMicroservice.Contracts
{
    public interface IDateService
    {
        String ConvertDateToString(DateTime date);
    }
}
