using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcquisitionGateway.Entities
{
    public class AirQualityData
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
    }
}
