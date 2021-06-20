using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrafficMicroservice.Contracts
{
    public interface IGeolocationService
    {
        double CalculateDistance(double latitude1, double longitude1, double latitude2, double longitude2);
    }
}
