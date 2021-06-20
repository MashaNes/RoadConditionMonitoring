using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleMonitoringGateway.Entities
{
    public class VehicleLocationData
    {
        public VehicleData VehicleData { get; set; }
        public List<LocationData> LocationDataList { get; set; }
        public List<LocationTrafficData> LocationTrafficDataList { get; set; }
    }
}
