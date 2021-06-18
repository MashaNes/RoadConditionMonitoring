using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleMonitoringGateway.Entities
{
    public class LocationData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Temperature Temperature { get; set; }
        public AirQuality AirQuality { get; set; }
    }
}
