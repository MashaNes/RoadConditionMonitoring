using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Entities;
using MonitoringGateway.DTOs;

namespace MonitoringGateway.Contracts
{
    public interface ILocationDataService
    {
        Task<List<LocationData>> GetNewest(LocationRadiusDTO locationInfo);
        Task<List<AverageLocationData>> GetAverageH(LocationRadiusDTO locationInfo);
        Task<List<AverageLocationData>> GetAverageDay(LocationRadiusDTO locationInfo);
        Task<List<AverageLocationData>> GetAverage(AverageDTO averageInfo);
        Task<List<LocationDataList>> GetAll(LocationRadiusDTO locationInfo);
        Task<List<LocationDataList>> GetTimeframe(LocationTimeDTO locationTimeInfo);
    }
}
