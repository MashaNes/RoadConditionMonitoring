using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringGateway.Entities.Acquisition
{
    public class LocationTrafficData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Radius { get; set; }
        public TrafficData TrafficData { get; set; }
    }
}
