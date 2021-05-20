using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Entities.Acquisition;

namespace MonitoringGateway.Entities
{
    public class AverageLocationData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public AverageTemperature AverageTemperature { get; set; }
        public AverageAirQuality AverageAirQuality { get; set; }

        public AverageLocationData()
        { }

        public AverageLocationData(AverageAirQualityData airQualityData)
        {
            Latitude = airQualityData.Latitude;
            Longitude = airQualityData.Longitude;
            AverageAirQuality = new AverageAirQuality(airQualityData);
        }

        public AverageLocationData(AverageTempData roadAndAirTempData)
        {
            Latitude = roadAndAirTempData.Latitude;
            Longitude = roadAndAirTempData.Longitude;
            AverageTemperature = new AverageTemperature(roadAndAirTempData);
        }
    }
}
