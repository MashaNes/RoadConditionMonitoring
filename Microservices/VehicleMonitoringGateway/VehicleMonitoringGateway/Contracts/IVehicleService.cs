using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleMonitoringGateway.Entities;

namespace VehicleMonitoringGateway.Contracts
{
    public interface IVehicleService
    {
        Task<VehicleLocationData> getInfoForVehicle(int vehicleId);
    }
}
