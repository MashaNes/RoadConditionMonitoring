using Geolocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrafficMicroservice.Contracts;

namespace TrafficMicroservice.Services
{
    public class GeolocationService : IGeolocationService
    {
        public double CalculateDistance(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            return GeoCalculator.GetDistance(
                new Coordinate { Latitude = latitude1, Longitude = longitude1 },
                new Coordinate { Latitude = latitude2, Longitude = longitude2 },
                3, DistanceUnit.Meters);
        }
    }
}
