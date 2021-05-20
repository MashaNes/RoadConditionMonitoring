using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Entities.Acquisition;

namespace MonitoringGateway.Entities
{
    public class LocationData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Temperature Temperature { get; set; }
        public AirQuality AirQuality { get; set; }

        public LocationData()
        { }

        public LocationData(AirQualityData airQualityData)
        {
            Latitude = airQualityData.Latitude;
            Longitude = airQualityData.Longitude;
            AirQuality = new AirQuality(airQualityData);
        }

        public LocationData(RoadAndAirTempData roadAndAirTempData)
        {
            Latitude = roadAndAirTempData.Latitude;
            Longitude = roadAndAirTempData.Longitude;
            Temperature = new Temperature(roadAndAirTempData);
        }
    }
}
