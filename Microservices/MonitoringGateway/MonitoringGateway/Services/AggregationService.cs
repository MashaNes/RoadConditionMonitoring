using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Contracts;
using MonitoringGateway.Entities;
using MonitoringGateway.Entities.Acquisition;

namespace MonitoringGateway.Services
{
    public class AggregationService : IAggregationService
    {
        private readonly IGeolocationService _geolocationService;

        public AggregationService(IGeolocationService geolocationService)
        {
            this._geolocationService = geolocationService;
        }

        public List<LocationData> AggregateTempAirQuality(List<RoadAndAirTempData> tempData, List<AirQualityData> airQualityData)
        {
            List<LocationData> locationData = tempData.Select(temp => new LocationData(temp)).ToList();

            foreach (AirQualityData data in airQualityData)
            {
                LocationData loc = locationData.Find(loc => _geolocationService.CalculateDistance(loc.Latitude, loc.Longitude, data.Latitude, data.Longitude) 
                                                                <= _geolocationService.MaxDistance);
                if (loc != null)
                    loc.AirQuality = new AirQuality(data);
                else
                    locationData.Add(new LocationData(data));
            }
            return locationData;
        }

        public List<AverageLocationData> AggregateAverage(List<AverageTempData> tempData, List<AverageAirQualityData> airQualityData)
        {
            List<AverageLocationData> locationData = tempData.Select(temp => new AverageLocationData(temp)).ToList();

            foreach (AverageAirQualityData data in airQualityData)
            {
                AverageLocationData loc = locationData.Find(loc => _geolocationService.CalculateDistance(loc.Latitude, loc.Longitude, data.Latitude, data.Longitude)
                                                                <= _geolocationService.MaxDistance);
                if (loc != null)
                    loc.AverageAirQuality = new AverageAirQuality(data);
                else
                    locationData.Add(new AverageLocationData(data));
            }
            return locationData;
        }
    }
}
