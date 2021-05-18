using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirQualityMicroservice.DTOs
{
    public class LocationTimeDTO
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double RadiusMeters { get; set; }
        public DateTime ReferenceTime { get; set; }
        public int Seconds { get; set; }
    }
}
