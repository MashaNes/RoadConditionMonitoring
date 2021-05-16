using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcquisitionGateway.Entities;

namespace AcquisitionGateway.Contracts
{
    public interface IAirQualityService
    {
        Task<bool> AddData(AirQualityData newData);
    }
}
