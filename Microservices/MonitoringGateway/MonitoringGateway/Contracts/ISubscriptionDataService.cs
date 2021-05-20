using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Entities;

namespace MonitoringGateway.Contracts
{
    public interface ISubscriptionDataService
    {
        Task<List<LocationData>> GetNewest();
        Task<List<AverageLocationData>> GetAverageH();
        Task<List<AverageLocationData>> GetAverageDay();
    }
}
