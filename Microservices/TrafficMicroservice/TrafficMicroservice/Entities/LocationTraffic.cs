using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrafficMicroservice.Entities
{
    public class LocationTraffic
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public String ParamName { get; set; }
        public double Value { get; set; }
        public double Radius { get; set; }
    }
}
