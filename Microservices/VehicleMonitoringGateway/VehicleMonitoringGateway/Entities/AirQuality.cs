using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleMonitoringGateway.Entities
{
    public class AirQuality
    {
        public String StationName { get; set; }
        public DateTime Timestamp { get; set; }
        public double CO { get; set; }
        public double NMHC { get; set; }
        public double Benzene { get; set; }
        public double NOx { get; set; }
        public double NO2 { get; set; }
        public double RelativeHumidity { get; set; }
    }
}
