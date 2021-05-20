using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Entities.Acquisition;

namespace MonitoringGateway.Contracts
{
    public interface ITemperatureService
    {
        Task<List<RoadAndAirTempData>> GetNewest();
        Task<List<AverageTempData>> GetAveragePerH();
        Task<List<AverageTempData>> GetAveragePerDay();
    }
}
