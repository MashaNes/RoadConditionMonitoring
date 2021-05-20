using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Entities.Acquisition;

namespace MonitoringGateway.Contracts
{
    public interface IAirQualityService
    {
        Task<List<AirQualityData>> GetNewest();
        Task<List<AverageAirQualityData>> GetAveragePerH();
        Task<List<AverageAirQualityData>> GetAveragePerDay();
    }
}
