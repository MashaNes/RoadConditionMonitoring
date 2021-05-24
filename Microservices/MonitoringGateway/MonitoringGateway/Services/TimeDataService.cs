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
    public class TimeDataService : ITimeDataService
    {
        private readonly ITemperatureService _temperatureService;
        private readonly IAirQualityService _airQualityService;
        private readonly IAggregationService _aggregationService;

        public TimeDataService(ITemperatureService temperatureService, IAirQualityService airQualityService, IAggregationService aggregationService)
        {
            this._airQualityService = airQualityService;
            this._temperatureService = temperatureService;
            this._aggregationService = aggregationService;
        }

        public async Task<List<AverageLocationData>> GetAverage(DateTime dateTime, bool perHour)
        {
            List<AverageTempData> temps = await _temperatureService.GetAverageDate(dateTime, perHour);
            List<AverageAirQualityData> airQualities = await _airQualityService.GetAverageDate(dateTime, perHour);

            return _aggregationService.AggregateAverage(temps, airQualities);
        }

        public async Task<List<LocationDataList>> GetTimeframe(TimeframeDTO timeInfo)
        {
            List<RoadAndAirTempData> temps = await _temperatureService.GetTimeframe(timeInfo);
            List<AirQualityData> airQualities = await _airQualityService.GetTimeframe(timeInfo);

            return _aggregationService.AggregateTempAirQualityList(temps, airQualities);
        }
    }
}
