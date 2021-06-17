using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleLocationMicroservice.Entities
{
    public class VehicleData
    {
        public int VehicleId { get; set; }
        public double VehicleSpeed { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }

        public override string ToString()
        {
            string data = "VehicleId = " + VehicleId + "\n";
            data += "Latitude = " + Latitude + "\n";
            data += "Longitude = " + Longitude + "\n";
            data += "Timestamp = " + Timestamp.ToString() + "\n";
            return data;
        }
    }
}
