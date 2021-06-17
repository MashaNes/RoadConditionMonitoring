using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleGateway.Entities
{
    public class VehicleData
    {
        public int VehicleId { get; set; }
        public double VehicleSpeed { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
