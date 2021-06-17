using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirQualityMicroservice.Contracts;

namespace AirQualityMicroservice.Entities
{
    public class AirQualityData : ILocation
    {
        public String StationName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
        public double CO { get; set; }
        public double NMHC { get; set; }
        public double Benzene { get; set; }
        public double NOx { get; set; }
        public double NO2 { get; set; }
        public double RelativeHumidity { get; set; }

        public override string ToString()
        {
            string print = "StationName: " + StationName + "\n";
            print += "Latitude: " + Latitude + "\n";
            print += "Longitude: " + Longitude + "\n";
            print += "Timestamp: " + Timestamp.ToString() + "\n";
            print += "CO: " + CO + "\n";
            print += "NMHC: " + NMHC + "\n";
            print += "Benzene: " + Benzene + "\n";
            print += "NOx: " + NOx + "\n";
            print += "NO2: " + NO2 + "\n";
            print += "RelativeHumidity: " + RelativeHumidity + "\n";
            return print;
        }
    }
}
