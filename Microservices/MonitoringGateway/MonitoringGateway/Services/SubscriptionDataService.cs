using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Contracts;
using MonitoringGateway.Entities;
using MonitoringGateway.Entities.Acquisition;
using Geolocation;

namespace MonitoringGateway.Services
{
    public class SubscriptionDataService : ISubscriptionDataService
    {
        private readonly ITemperatureService _temperatureService;
        private readonly IAirQualityService _airQualityService;
        private readonly IAggregationService _aggregationService;

        public SubscriptionDataService(ITemperatureService temperatureService, IAirQualityService airQualityService, IAggregationService aggregationService)
        {
            this._airQualityService = airQualityService;
            this._temperatureService = temperatureService;
            this._aggregationService = aggregationService;
        }

        public async Task<List<LocationData>> GetNewest()
        {
            List<RoadAndAirTempData> temps = await _temperatureService.GetNewest();
            List<AirQualityData> airQualities = await _airQualityService.GetNewest();

            return _aggregationService.AggregateTempAirQuality(temps, airQualities);
        }

        public async Task<List<AverageLocationData>> GetAverageH()
        {
            List<AverageTempData> temps = await _temperatureService.GetAveragePerH();
            List<AverageAirQualityData> airQualities = await _airQualityService.GetAveragePerH();

            return _aggregationService.AggregateAverage(temps, airQualities);
        }

        public async Task<List<AverageLocationData>> GetAverageDay()
        {
            List<AverageTempData> temps = await _temperatureService.GetAveragePerDay();
            List<AverageAirQualityData> airQualities = await _airQualityService.GetAveragePerDay();

            return _aggregationService.AggregateAverage(temps, airQualities);
        }
    }
}
