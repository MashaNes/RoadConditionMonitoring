using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrafficMicroservice.Contracts;
using TrafficMicroservice.Entities;

namespace TrafficMicroservice.Services
{
    public class TrafficService : ITrafficService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICassandraService _cassandraService;
        private readonly IGeolocationService _geolocationService;

        public TrafficService(IUnitOfWork unitOfWork,ICassandraService cassandraService, IGeolocationService geolocationService)
        {
            this._unitOfWork = unitOfWork;
            this._cassandraService = cassandraService;
            this._geolocationService = geolocationService;
        }

        public TrafficData GetGlobalData()
        {
            GlobalTraffic numVehicles =_cassandraService.ConvertCassandraGlobalRow(
                _unitOfWork.CassandraSession.Execute(_cassandraService.SelectWhereQuery(_unitOfWork.TableGeneral, "paramname", "vehicle_number"))
                                            .FirstOrDefault());
            if (numVehicles is null || numVehicles.Value == 0)
                return new TrafficData() { VehicleNumber = 0, AverageSpeed = 0 };
            
            GlobalTraffic speedCount = _cassandraService.ConvertCassandraGlobalRow(
                _unitOfWork.CassandraSession.Execute(_cassandraService.SelectWhereQuery(_unitOfWork.TableGeneral, "paramname", "speed_count"))
                                            .FirstOrDefault());
            GlobalTraffic speedTotal = _cassandraService.ConvertCassandraGlobalRow(
                _unitOfWork.CassandraSession.Execute(_cassandraService.SelectWhereQuery(_unitOfWork.TableGeneral, "paramname", "speed_total"))
                                            .FirstOrDefault());
            
            return new TrafficData() { VehicleNumber = (int)numVehicles.Value, AverageSpeed = speedTotal.Value / speedCount.Value };
        }

        public List<LocationTrafficData> GetAllAvailableLocationData()
        {
            List<LocationTrafficData> locationTrafficData = new List<LocationTrafficData>();

            var allLocations = _unitOfWork.CassandraSession.Execute(_cassandraService.SelectWhereQuery(_unitOfWork.TableLocation, "paramname", "vehicle_number"));
            foreach(var instance in allLocations)
            {
                LocationTraffic locationTraffic = _cassandraService.ConvertCassandraLocationRow(instance);
                if(locationTraffic.Value > 0)
                    locationTrafficData.Add(GetAllInfo(locationTraffic));
            }

            return locationTrafficData;
        }

        public List<LocationTrafficData> GetNearAvailableLocationData(double Latitude, double Longitude, double Radius)
        {
            List<LocationTrafficData> locationTrafficData = new List<LocationTrafficData>();

            var allLocations = _unitOfWork.CassandraSession.Execute(_cassandraService.SelectWhereQuery(_unitOfWork.TableLocation, "paramname", "vehicle_number"));
            foreach (var instance in allLocations)
            {
                LocationTraffic locationTraffic = _cassandraService.ConvertCassandraLocationRow(instance);
                if (locationTraffic.Value > 0 && _geolocationService.CalculateDistance(locationTraffic.Latitude, locationTraffic.Longitude, Latitude, Longitude) <= Radius)
                    locationTrafficData.Add(GetAllInfo(locationTraffic));
            }

            return locationTrafficData;
        }

        private LocationTrafficData GetAllInfo(LocationTraffic locationTraffic)
        {
            LocationTraffic speedCount =
                        _cassandraService.ConvertCassandraLocationRow(
                            _unitOfWork.CassandraSession.Execute(
                                _cassandraService.SelectWhereLocationQuery(_unitOfWork.TableLocation, locationTraffic.Latitude, locationTraffic.Longitude, "paramname", "speed_count"))
                                .FirstOrDefault());
            LocationTraffic speedTotal =
                _cassandraService.ConvertCassandraLocationRow(
                    _unitOfWork.CassandraSession.Execute(
                        _cassandraService.SelectWhereLocationQuery(_unitOfWork.TableLocation, locationTraffic.Latitude, locationTraffic.Longitude, "paramname", "speed_total"))
                        .FirstOrDefault());
            
            return new LocationTrafficData()
            {
                Latitude = locationTraffic.Latitude,
                Longitude = locationTraffic.Longitude,
                Radius = locationTraffic.Radius,
                TrafficData = new TrafficData()
                {
                    VehicleNumber = (int)locationTraffic.Value,
                    AverageSpeed = speedTotal.Value / speedCount.Value
                }
            };
        }
    }
}
