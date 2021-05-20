using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Entities;
using MonitoringGateway.Entities.Acquisition;

namespace MonitoringGateway.Contracts
{
    public interface IAggregationService
    {
        List<LocationData> AggregateTempAirQuality(List<RoadAndAirTempData> tempData, List<AirQualityData> airQualityData);
        List<AverageLocationData> AggregateAverage(List<AverageTempData> tempData, List<AverageAirQualityData> airQualityData);
    }
}
