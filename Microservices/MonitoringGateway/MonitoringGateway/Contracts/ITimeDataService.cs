using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Entities;
using MonitoringGateway.DTOs;

namespace MonitoringGateway.Contracts
{
    public interface ITimeDataService
    {
        Task<List<AverageLocationData>> GetAverage(DateTime dateTime, bool perHour);
        Task<List<LocationDataList>> GetTimeframe(TimeframeDTO timeInfo);
    }
}
