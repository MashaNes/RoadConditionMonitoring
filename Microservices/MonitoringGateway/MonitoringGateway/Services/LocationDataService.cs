using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Contracts;
using MonitoringGateway.DTOs;
using MonitoringGateway.Entities;
using MonitoringGateway.Entities.Acquisition;

namespace MonitoringGateway.Services
{
    public class LocationDataService : ILocationDataService
    {
        private readonly ITemperatureService _temperatureService;
        private readonly IAirQualityService _airQualityService;
        private readonly IAggregationService _aggregationService;

        public LocationDataService(ITemperatureService temperatureService, IAirQualityService airQualityService, IAggregationService aggregationService)
        {
            this._airQualityService = airQualityService;
            this._temperatureService = temperatureService;
            this._aggregationService = aggregationService;
        }

        public async Task<List<LocationData>> GetNewest(LocationRadiusDTO locationInfo)
        {
            List<RoadAndAirTempData> temps = await _temperatureService.GetNewestLocation(locationInfo);
            List<AirQualityData> airQualities = await _airQualityService.GetNewestLocation(locationInfo);

            return _aggregationService.AggregateTempAirQuality(temps, airQualities);
        }

        public async Task<List<AverageLocationData>> GetAverageH(LocationRadiusDTO locationInfo)
        {
            List<AverageTempData> temps = await _temperatureService.GetAverageLocationHour(locationInfo);
            List<AverageAirQualityData> airQualities = await _airQualityService.GetAverageLocationHour(locationInfo);

            return _aggregationService.AggregateAverage(temps, airQualities);
        }

        public async Task<List<AverageLocationData>> GetAverageDay(LocationRadiusDTO locationInfo)
        {
            List<AverageTempData> temps = await _temperatureService.GetAverageLocationDay(locationInfo);
            List<AverageAirQualityData> airQualities = await _airQualityService.GetAverageLocationDay(locationInfo);

            return _aggregationService.AggregateAverage(temps, airQualities);
        }

        public async Task<List<AverageLocationData>> GetAverage(AverageDTO averageInfo)
        {
            List<AverageTempData> temps = await _temperatureService.GetAverageLocation(averageInfo);
            List<AverageAirQualityData> airQualities = await _airQualityService.GetAverageLocation(averageInfo);

            return _aggregationService.AggregateAverage(temps, airQualities);
        }

        public async Task<List<LocationDataList>> GetAll(LocationRadiusDTO locationInfo)
        {
            List<RoadAndAirTempData> temps = await _temperatureService.GetAllLocation(locationInfo);
            List<AirQualityData> airQualities = await _airQualityService.GetAllLocation(locationInfo);

            return _aggregationService.AggregateTempAirQualityList(temps, airQualities);
        }

        public async Task<List<LocationDataList>> GetTimeframe(LocationTimeDTO locationTimeInfo)
        {
            List<RoadAndAirTempData> temps = await _temperatureService.GetTimeframeLocation(locationTimeInfo);
            List<AirQualityData> airQualities = await _airQualityService.GetTimeframeLocation(locationTimeInfo);

            return _aggregationService.AggregateTempAirQualityList(temps, airQualities);
        }
    }
}
