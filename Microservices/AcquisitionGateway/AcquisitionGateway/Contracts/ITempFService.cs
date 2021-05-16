using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcquisitionGateway.Entities;

namespace AcquisitionGateway.Contracts
{
    public interface ITempFService
    {
        Task<bool> AddData(RoadAndAirTempData newData);
    }
}
