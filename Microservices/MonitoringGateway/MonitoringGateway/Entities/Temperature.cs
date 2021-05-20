using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Entities.Acquisition;

namespace MonitoringGateway.Entities
{
    public class Temperature
    {
        public String StationName { get; set; }
        public DateTime Timestamp { get; set; }
        public double AirTemperature { get; set; }
        public double RoadTemperature { get; set; }

        public Temperature()
        { }

        public Temperature(RoadAndAirTempData roadAndAirTempData)
        {
            StationName = roadAndAirTempData.StationName;
            Timestamp = roadAndAirTempData.Timestamp;
            AirTemperature = roadAndAirTempData.AirTemperature;
            RoadTemperature = roadAndAirTempData.RoadTemperature;
        }
    }
}
