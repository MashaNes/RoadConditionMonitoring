using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleGateway.Entities;

namespace VehicleGateway.Contracts
{
    public interface ISpeedMSService
    {
        Task<bool> AddData(VehicleData newData);
    }
}
