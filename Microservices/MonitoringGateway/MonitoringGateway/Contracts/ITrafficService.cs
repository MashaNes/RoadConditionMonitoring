using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Entities;
using MonitoringGateway.Entities.Acquisition;
using MonitoringGateway.DTOs;

namespace MonitoringGateway.Contracts
{
    public interface ITrafficService
    {
        Task<AllTrafficData> GetAllTrafficData();
        Task<List<LocationTrafficData>> GetLocationTrafficData(LocationRadiusDTO locationInfo);
    }
}
