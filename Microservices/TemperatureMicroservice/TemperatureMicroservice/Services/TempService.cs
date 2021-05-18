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
        private readonly IGeolocationService _geolocationService;

        public TempService(IUnitOfWork unitOfWork, ICassandraService cassandraService, IGeolocationService geolocationService)
        {
            this._unitOfWork = unitOfWork;
            this._cassandraService = cassandraService;
            this._geolocationService = geolocationService;
        }

        public async Task<List<RoadAndAirTempData>> GetAllNewest()
        {
            var data = _unitOfWork.CassandraSession.Execute(_cassandraService.SelectAllQuery(_unitOfWork.TableNewestData));
            return data.Select(instance => _cassandraService.ConvertCassandraTempRow(instance)).ToList();
        }

        public async Task<List<RoadAndAirTempData>> GetDataByTimeframe(DateTime time, int seconds)
        {
            DateTime low = time.AddSeconds(-seconds);
            DateTime high = time.AddSeconds(seconds);

            var data = _unitOfWork.CassandraSession.Execute(_cassandraService.SelectTimeframeQuery(_unitOfWork.TableAllData, low, high));
            return data.Select(instance => _cassandraService.ConvertCassandraTempRow(instance)).ToList();
        }

        public async Task<List<RoadAndAirTempData>> GetDataLocation(LocationRadiusDTO locationInfo, bool newest)
        {
            string table = newest ? _unitOfWork.TableNewestData : _unitOfWork.TableAllData;
            var data = _unitOfWork.CassandraSession.Execute(_cassandraService.SelectAllQuery(table));
            return FilterByLocation(locationInfo, data.Select(instance => _cassandraService.ConvertCassandraTempRow(instance)).ToList());
        }

        public async Task<List<RoadAndAirTempData>> GetDataTimeframeLocation(LocationTimeDTO info)
        {
            DateTime low = info.ReferenceTime.AddSeconds(-info.Seconds);
            DateTime high = info.ReferenceTime.AddSeconds(info.Seconds);

            var data = _unitOfWork.CassandraSession.Execute(_cassandraService.SelectTimeframeQuery(_unitOfWork.TableAllData, low, high));
            return FilterByLocation<RoadAndAirTempData>(new LocationRadiusDTO(info), data.Select(instance => _cassandraService.ConvertCassandraTempRow(instance)).ToList());
        }

        public async Task<List<AverageTempData>> GetAverageData(AverageDTO info)
        {
            string table = info.PerHour ? _unitOfWork.TableAverageH : _unitOfWork.TableAverageDay;
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
                if (_geolocationService.CalculateDistance(instance.Latitude, instance.Longitude, locationInfo.Latitude, locationInfo.Longitude) <= locationInfo.RadiusMeters)
                    retList.Add(instance);
            }
            return retList;
        }
    }
}
