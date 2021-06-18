using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleMonitoringGateway.Entities
{
    public class Temperature
    {
        public String StationName { get; set; }
        public DateTime Timestamp { get; set; }
        public double AirTemperature { get; set; }
        public double RoadTemperature { get; set; }
    }
}
