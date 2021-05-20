using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringGateway.Entities.Acquisition
{
    public class AverageAirQualityData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Radius { get; set; }
        public DateTime Timestamp { get; set; }
        public double AverageCO { get; set; }
        public double AverageNMHC { get; set; }
        public double AverageBenzene { get; set; }
        public double AverageNOx { get; set; }
        public double AverageNO2 { get; set; }
        public double AverageRelativeHumidity { get; set; }
        public int DataCount { get; set; }
    }
}
