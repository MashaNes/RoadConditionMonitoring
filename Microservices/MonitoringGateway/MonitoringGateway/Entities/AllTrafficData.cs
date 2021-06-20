using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Entities.Acquisition;

namespace MonitoringGateway.Entities
{
    public class AllTrafficData
    {
        public TrafficData GlobalTrafficData { get; set; }
        public List<LocationTrafficData> LocationTrafficData { get; set; }
    }
}
