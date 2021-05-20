using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Entities.Acquisition;

namespace MonitoringGateway.Entities
{
    public class AverageTemperature
    {
        public double AverageAirTemperature { get; set; }
        public double AverageRoadTemperature { get; set; }
        public int DataCount { get; set; }

        public AverageTemperature()
        { }

        public AverageTemperature(AverageTempData averageTempData)
        {
            AverageAirTemperature = averageTempData.AverageAirTemperature;
            AverageRoadTemperature = averageTempData.AverageRoadTemperature;
            DataCount = averageTempData.DataCount;
        }
    }
}
