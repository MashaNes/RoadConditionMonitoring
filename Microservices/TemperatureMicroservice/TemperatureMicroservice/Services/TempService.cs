using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemperatureMicroservice.Contracts;
using TemperatureMicroservice.Entities;
using Cassandra;
using Geolocation;

namespace TemperatureMicroservice.Services
{
    public class TempService : ITempService
    {
        private readonly IUnitOfWork _unitOfWork;
        private static int MaxDistance = 5;

        public TempService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<bool> AddData(RoadAndAirTempData newData)
        {
            _unitOfWork.CassandraSession.Execute(ICassandraService.InsertRoadAndAirTempDataQuery("all_temperatures", newData));

            bool found = false;
            Coordinate newPoint = new Coordinate(newData.Latitude, newData.Longitude);

            var DataNewestt = _unitOfWork.CassandraSession.Execute(ICassandraService.SelectAllQuery("newest_temperatures"));
            foreach (var instance in DataNewestt)
            {
                RoadAndAirTempData roadData = ICassandraService.ConvertCassandraTempRow(instance);
                Coordinate point = new Coordinate(roadData.Latitude, roadData.Longitude);
                double distance = GeoCalculator.GetDistance(newPoint, point, 3, DistanceUnit.Meters);
                if (distance <= MaxDistance)
                {
                    found = true;
                    if (newData.Timestamp > roadData.Timestamp)
                        _unitOfWork.CassandraSession.Execute(ICassandraService.UpdateNewestQuery("newest_temperatures", newData, roadData.Latitude, roadData.Longitude));
                }
            }
            if (!found)
                _unitOfWork.CassandraSession.Execute(ICassandraService.InsertRoadAndAirTempDataQuery("newest_temperatures", newData));

            found = false;
            newData.Timestamp = new DateTime(newData.Timestamp.Year, newData.Timestamp.Month, newData.Timestamp.Day).AddHours(newData.Timestamp.Hour);

            var DataHour = _unitOfWork.CassandraSession.Execute(ICassandraService.SelectAllQuery("average_per_h"));
            foreach (var instance in DataHour)
            {
                AverageTempData averageData = ICassandraService.ConvertCassandraAverageRow(instance);
                Coordinate point = new Coordinate(averageData.Latitude, averageData.Longitude);
                double distance = GeoCalculator.GetDistance(newPoint, point, 3, DistanceUnit.Meters);
                if (distance <= averageData.Radius)
                {
                    if(averageData.Timestamp == newData.Timestamp)
                    {
                        found = true;
                        _unitOfWork.CassandraSession.Execute(ICassandraService.UpdateAverageQuery("average_per_h", newData, averageData));
                    }                  
                }
            }
            if (!found)
                _unitOfWork.CassandraSession.Execute(ICassandraService.InsertAverageTempDataQuery("average_per_h", newData, MaxDistance));

            found = false;
            newData.Timestamp = new DateTime(newData.Timestamp.Year, newData.Timestamp.Month, newData.Timestamp.Day);

            var DataDay = _unitOfWork.CassandraSession.Execute(ICassandraService.SelectAllQuery("average_per_day"));
            foreach (var instance in DataDay)
            {
                AverageTempData averageData = ICassandraService.ConvertCassandraAverageRow(instance);
                Coordinate point = new Coordinate(averageData.Latitude, averageData.Longitude);
                double distance = GeoCalculator.GetDistance(newPoint, point, 3, DistanceUnit.Meters);
                if (distance <= averageData.Radius)
                {
                    if (averageData.Timestamp == newData.Timestamp)
                    {
                        found = true;
                        _unitOfWork.CassandraSession.Execute(ICassandraService.UpdateAverageQuery("average_per_day", newData, averageData));
                    }
                }
            }
            if (!found)
                _unitOfWork.CassandraSession.Execute(ICassandraService.InsertAverageTempDataQuery("average_per_day", newData, MaxDistance));

            return true;
        }

        public async Task<List<RoadAndAirTempData>> GetAllData()
        {
            List<RoadAndAirTempData> retList = new List<RoadAndAirTempData>();

            var data = _unitOfWork.CassandraSession.Execute(ICassandraService.SelectAllQuery("all_temperatures"));

            foreach (var instance in data)
            {
                retList.Add(ICassandraService.ConvertCassandraTempRow(instance));
            }

            return retList;
        }

        public async Task<RoadAndAirTempData> GetDataByRecordId(int recordId)
        {
            Row result = _unitOfWork.CassandraSession.Execute("select * from all_temperatures where \"RecordId\"=" + recordId + " ALLOW FILTERING").FirstOrDefault();

            return ICassandraService.ConvertCassandraTempRow(result);
        }

        public async Task<List<RoadAndAirTempData>> GetDataByStationName(String StationName, bool Newest)
        {
            List<RoadAndAirTempData> retList = new List<RoadAndAirTempData>();

            var data = _unitOfWork.CassandraSession.Execute(ICassandraService.SelectWhereQuery("all_temperatures", "StationName", "'" + StationName + "'"));

            RoadAndAirTempData newestInfo = null;

            foreach (var instance in data)
            {
                RoadAndAirTempData roadData = ICassandraService.ConvertCassandraTempRow(instance);

                if (!Newest)
                    retList.Add(roadData);
                else if (newestInfo == null || roadData.Timestamp > newestInfo.Timestamp)
                    newestInfo = roadData;
            }

            if (Newest)
                retList.Add(newestInfo);

            return retList;
        }

        public async Task<List<RoadAndAirTempData>> GetDataByTimestamp(String StationName, DateTime time, int seconds)
        {
            if (StationName == null)
                StationName = "";
            else
                StationName = "and \"StationName\"='" + StationName + "' ";

            DateTime low = time.AddSeconds(-seconds);
            DateTime high = time.AddSeconds(seconds);

            List<RoadAndAirTempData> retList = new List<RoadAndAirTempData>();

            var data = _unitOfWork.CassandraSession.Execute("select * from all_temperatures where \"Timestamp\" > '" + IDateService.ConvertDateToString(low)
                     + "' and \"Timestamp\" < '" + IDateService.ConvertDateToString(high) + "' " + StationName + "ALLOW FILTERING");

            foreach (var instance in data)
            {
                retList.Add(ICassandraService.ConvertCassandraTempRow(instance));
            }

            return retList;
        }

        /*public async Task<List<RoadAndAirTempData>> GetDataLocation(LocationRadiusDTO locationInfo)
        {
            List<RoadAndAirTempData> retList = new List<RoadAndAirTempData>();

            Coordinate center = new Coordinate(locationInfo.CenterLatitude, locationInfo.CenterLongitude);

            var data = _unitOfWork.CassandraSession.Execute("select * from all_temperatures");
            RoadAndAirTempData newest = null;

            foreach (var instance in data)
            {
                RoadAndAirTempData roadData = ConvertCassandraRow(instance);
                Coordinate point = new Coordinate(roadData.Latitude, roadData.Longitude);
                double distance = GeoCalculator.GetDistance(center, point, 3, DistanceUnit.Meters);
                if (distance <= locationInfo.RadiusMeters)
                {
                    if (!locationInfo.Newest)
                        retList.Add(roadData);
                    else if (newest == null || newest.Timestamp < roadData.Timestamp)
                        newest = roadData;
                }
            }

            if (locationInfo.Newest)
                retList.Add(newest);

            return retList;
        }*/
    }
}
