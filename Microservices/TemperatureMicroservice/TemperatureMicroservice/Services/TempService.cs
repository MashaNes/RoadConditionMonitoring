using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemperatureMicroservice.Contracts;
using TemperatureMicroservice.Entities;
using Cassandra;
using Geolocation;
using TemperatureMicroservice.DTOs;

namespace TemperatureMicroservice.Services
{
    public class TempService : ITempService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICassandraService _cassandraService;
        private static int MaxDistance = 5;

        public TempService(IUnitOfWork unitOfWork, ICassandraService cassandraService)
        {
            this._unitOfWork = unitOfWork;
            this._cassandraService = cassandraService;
        }

        public async Task<bool> AddData(RoadAndAirTempData newData)
        {
            _unitOfWork.CassandraSession.Execute(_cassandraService.InsertRoadAndAirTempDataQuery("all_temperatures", newData));

            bool found = false;

            var DataNewest = _unitOfWork.CassandraSession.Execute(_cassandraService.SelectAllQuery("newest_temperatures"));
            foreach (var instance in DataNewest)
            {
                RoadAndAirTempData roadData = _cassandraService.ConvertCassandraTempRow(instance);
                if (CalculateDistance(newData.Latitude, newData.Longitude, roadData.Latitude, roadData.Longitude) <= MaxDistance)
                {
                    found = true;
                    if (newData.Timestamp > roadData.Timestamp)
                        _unitOfWork.CassandraSession.Execute(_cassandraService.UpdateNewestQuery("newest_temperatures", newData, roadData.Latitude, roadData.Longitude));
                }
            }
            if (!found)
                _unitOfWork.CassandraSession.Execute(_cassandraService.InsertRoadAndAirTempDataQuery("newest_temperatures", newData));

            newData.Timestamp = new DateTime(newData.Timestamp.Year, newData.Timestamp.Month, newData.Timestamp.Day).AddHours(newData.Timestamp.Hour);
            UpdateAverage("average_per_h", newData);

            newData.Timestamp = new DateTime(newData.Timestamp.Year, newData.Timestamp.Month, newData.Timestamp.Day);
            UpdateAverage("average_per_day", newData);

            return true;
        }

        private double CalculateDistance(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            return GeoCalculator.GetDistance(
                new Coordinate { Latitude = latitude1, Longitude = longitude1 },
                new Coordinate { Latitude = latitude2, Longitude = longitude2 },
                3, DistanceUnit.Meters);
        }

        private void UpdateAverage(string table, RoadAndAirTempData newData)
        {
            bool found = false;
            var data = _unitOfWork.CassandraSession.Execute(_cassandraService.SelectAllQuery(table));
            foreach (var instance in data)
            {
                AverageTempData averageData = _cassandraService.ConvertCassandraAverageRow(instance);
                if (CalculateDistance(newData.Latitude, newData.Longitude, averageData.Latitude, averageData.Longitude) <= averageData.Radius)
                {
                    if (DateTime.Compare(averageData.Timestamp, newData.Timestamp) == 0)
                    {
                        found = true;
                        _unitOfWork.CassandraSession.Execute(_cassandraService.UpdateAverageQuery(table, newData, averageData));
                    }
                }
            }
            if (!found)
                _unitOfWork.CassandraSession.Execute(_cassandraService.InsertAverageTempDataQuery(table, newData, MaxDistance));
        }

        public async Task<List<RoadAndAirTempData>> GetAllNewest()
        {
            var data = _unitOfWork.CassandraSession.Execute(_cassandraService.SelectAllQuery("newest_temperatures"));
            return data.Select(instance => _cassandraService.ConvertCassandraTempRow(instance)).ToList();
        }

        public async Task<List<RoadAndAirTempData>> GetDataByTimeframe(DateTime time, int seconds)
        {
            DateTime low = time.AddSeconds(-seconds);
            DateTime high = time.AddSeconds(seconds);

            var data = _unitOfWork.CassandraSession.Execute(_cassandraService.SelectTimeframeQuery("all_temperatures", low, high));
            return data.Select(instance => _cassandraService.ConvertCassandraTempRow(instance)).ToList();
        }

        public async Task<List<RoadAndAirTempData>> GetDataLocation(LocationRadiusDTO locationInfo, bool newest)
        {
            string table = newest ? "newest_temperatures" : "all_temperatures";
            var data = _unitOfWork.CassandraSession.Execute(_cassandraService.SelectAllQuery(table));
            return FilterByLocation(locationInfo, data.Select(instance => _cassandraService.ConvertCassandraTempRow(instance)).ToList());
        }

        public async Task<List<RoadAndAirTempData>> GetDataTimeframeLocation(LocationTimeDTO info)
        {
            DateTime low = info.ReferenceTime.AddSeconds(-info.Seconds);
            DateTime high = info.ReferenceTime.AddSeconds(info.Seconds);

            var data = _unitOfWork.CassandraSession.Execute(_cassandraService.SelectTimeframeQuery("all_temperatures", low, high));
            return FilterByLocation<RoadAndAirTempData>(new LocationRadiusDTO(info), data.Select(instance => _cassandraService.ConvertCassandraTempRow(instance)).ToList());
        }

        public async Task<List<AverageTempData>> GetAverageData(AverageDTO info)
        {
            string table = info.PerHour ? "average_per_h" : "average_per_day";
            var data = _unitOfWork.CassandraSession.Execute(_cassandraService.SelectByTimestampQuery(table, info.Timestamp));
            List<AverageTempData> retList = data.Select(instance => _cassandraService.ConvertCassandraAverageRow(instance)).ToList();
            if (info.LocationInfo is null)
                return retList;
            else
                return FilterByLocation<AverageTempData>(info.LocationInfo, retList);
        }

        private List<T> FilterByLocation<T>(LocationRadiusDTO locationInfo, List<T> data) where T : ILocation
        {
            List<T> retList = new List<T>();
            foreach(T instance in data)
            {
                if (CalculateDistance(instance.Latitude, instance.Longitude, locationInfo.Latitude, locationInfo.Longitude) <= locationInfo.RadiusMeters)
                    retList.Add(instance);
            }
            return retList;
        }
    }
}
