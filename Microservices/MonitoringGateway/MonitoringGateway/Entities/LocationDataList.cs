using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Entities.Acquisition;

namespace MonitoringGateway.Entities
{
    public class LocationDataList
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<Temperature> Temperatures { get; set; }
        public List<AirQuality> AirQualities { get; set; }

        public LocationDataList()
        {
            Temperatures = new List<Temperature>();
            AirQualities = new List<AirQuality>();
        }

        public LocationDataList(AirQualityData airQualityData) : this()
        {
            Latitude = airQualityData.Latitude;
            Longitude = airQualityData.Longitude;
            AirQualities.Add(new AirQuality(airQualityData));
        }

        public LocationDataList(RoadAndAirTempData roadAndAirTempData) : this()
        {
            Latitude = roadAndAirTempData.Latitude;
            Longitude = roadAndAirTempData.Longitude;
            Temperatures.Add(new Temperature(roadAndAirTempData));
        }
    }
}
