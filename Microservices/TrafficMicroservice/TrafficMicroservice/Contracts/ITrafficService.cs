using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrafficMicroservice.Entities;

namespace TrafficMicroservice.Contracts
{
    public interface ITrafficService
    {
        TrafficData GetGlobalData();
        List<LocationTrafficData> GetAllAvailableLocationData();
        List<LocationTrafficData> GetNearAvailableLocationData(double Latitude, double Longitude, double Radius);
    }
}
