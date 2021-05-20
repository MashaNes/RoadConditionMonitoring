using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Entities.Acquisition;

namespace MonitoringGateway.Entities
{
    public class AverageAirQuality
    {
        public double AverageCO { get; set; }
        public double AverageNMHC { get; set; }
        public double AverageBenzene { get; set; }
        public double AverageNOx { get; set; }
        public double AverageNO2 { get; set; }
        public double AverageRelativeHumidity { get; set; }
        public int DataCount { get; set; }

        public AverageAirQuality()
        { }

        public AverageAirQuality(AverageAirQualityData averageAirQualityData)
        {
            AverageCO = averageAirQualityData.AverageCO;
            AverageNMHC = averageAirQualityData.AverageNMHC;
            AverageBenzene = averageAirQualityData.AverageBenzene;
            AverageNOx = averageAirQualityData.AverageNOx;
            AverageNO2 = averageAirQualityData.AverageNO2;
            AverageRelativeHumidity = averageAirQualityData.AverageRelativeHumidity;
            DataCount = averageAirQualityData.DataCount;
        }
    }
}
