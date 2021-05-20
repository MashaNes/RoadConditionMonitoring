using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Entities.Acquisition;

namespace MonitoringGateway.Entities
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

        public AirQuality()
        { }

        public AirQuality(AirQualityData airQualityData)
        {
            StationName = airQualityData.StationName;
            Timestamp = airQualityData.Timestamp;
            CO = airQualityData.CO;
            NMHC = airQualityData.NMHC;
            Benzene = airQualityData.Benzene;
            NOx = airQualityData.NOx;
            NO2 = airQualityData.NO2;
            RelativeHumidity = airQualityData.RelativeHumidity;
        }
    }
}
