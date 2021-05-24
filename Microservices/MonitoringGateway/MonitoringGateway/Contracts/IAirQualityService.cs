using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Entities.Acquisition;
using MonitoringGateway.DTOs;

namespace MonitoringGateway.Contracts
{
    public interface IAirQualityService
    {
        Task<List<AirQualityData>> GetNewest();
        Task<List<AverageAirQualityData>> GetAveragePerH();
        Task<List<AverageAirQualityData>> GetAveragePerDay();
        Task<List<AirQualityData>> GetNewestLocation(LocationRadiusDTO locationInfo);
        Task<List<AverageAirQualityData>> GetAverageLocationHour(LocationRadiusDTO locationInfo);
        Task<List<AverageAirQualityData>> GetAverageLocationDay(LocationRadiusDTO locationInfo);
        Task<List<AverageAirQualityData>> GetAverageLocation(AverageDTO averageInfo);
        Task<List<AirQualityData>> GetAllLocation(LocationRadiusDTO locationInfo);
        Task<List<AirQualityData>> GetTimeframeLocation(LocationTimeDTO locationTimeInfo);
        Task<List<AverageAirQualityData>> GetAverageDate(DateTime dateTime, bool perHour);
        Task<List<AirQualityData>> GetTimeframe(TimeframeDTO timeInfo);
    }
}
