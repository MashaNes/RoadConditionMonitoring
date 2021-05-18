using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirQualityMicroservice.Entities;
using AirQualityMicroservice.DTOs;

namespace AirQualityMicroservice.Contracts
{
    public interface IAirQualityService
    {
        Task<List<AirQualityData>> GetAllNewest();
        Task<List<AirQualityData>> GetDataByTimeframe(DateTime time, int seconds);
        Task<List<AirQualityData>> GetDataLocation(LocationRadiusDTO locationInfo, bool newest);
        Task<List<AirQualityData>> GetDataTimeframeLocation(LocationTimeDTO info);
        Task<List<AverageAirQualityData>> GetAverageData(AverageDTO info);
    }
}
