using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Entities.Acquisition;
using MonitoringGateway.DTOs;

namespace MonitoringGateway.Contracts
{
    public interface ITemperatureService
    {
        Task<List<RoadAndAirTempData>> GetNewest();
        Task<List<AverageTempData>> GetAveragePerH();
        Task<List<AverageTempData>> GetAveragePerDay();
        Task<List<RoadAndAirTempData>> GetNewestLocation(LocationRadiusDTO locationInfo);
        Task<List<AverageTempData>> GetAverageLocationHour(LocationRadiusDTO locationInfo);
        Task<List<AverageTempData>> GetAverageLocationDay(LocationRadiusDTO locationInfo);
        Task<List<AverageTempData>> GetAverageLocation(AverageDTO averageInfo);
        Task<List<RoadAndAirTempData>> GetAllLocation(LocationRadiusDTO locationInfo);
        Task<List<RoadAndAirTempData>> GetTimeframeLocation(LocationTimeDTO locationTimeInfo);
        Task<List<AverageTempData>> GetAverageDate(DateTime dateTime, bool perHour);
        Task<List<RoadAndAirTempData>> GetTimeframe(TimeframeDTO timeInfo);
    }
}
