using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemperatureMicroservice.Entities;
using TemperatureMicroservice.DTOs;

namespace TemperatureMicroservice.Contracts
{
    public interface ITempService
    {
        Task<bool> AddData(RoadAndAirTempData newData);
        Task<List<RoadAndAirTempData>> GetAllNewest();
        Task<RoadAndAirTempData> GetDataByRecordId(int recordId);
        Task<List<RoadAndAirTempData>> GetDataByTimeframe(DateTime time, int seconds);
        Task<List<RoadAndAirTempData>> GetDataLocation(LocationRadiusDTO locationInfo, bool newest);
        Task<List<RoadAndAirTempData>> GetDataTimeframeLocation(LocationTimeDTO info);
        Task<List<AverageTempData>> GetAverageData(AverageDTO info);
    }
}
