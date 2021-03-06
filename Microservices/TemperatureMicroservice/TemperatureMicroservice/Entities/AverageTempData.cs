using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemperatureMicroservice.Contracts;

namespace TemperatureMicroservice.Entities
{
    public class AverageTempData : ILocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Radius { get; set; }
        public DateTime Timestamp { get; set; }
        public double AverageAirTemperature { get; set; }
        public double AverageRoadTemperature { get; set; }
        public int DataCount { get; set; }
    }
}
