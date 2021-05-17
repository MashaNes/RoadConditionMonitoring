using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cassandra;
using TemperatureMicroservice.Entities;
using Geolocation;

namespace TemperatureMicroservice.Contracts
{
    public interface ICassandraService
    {
        RoadAndAirTempData ConvertCassandraTempRow(Row instance);
        AverageTempData ConvertCassandraAverageRow(Row instance);
        string InsertRoadAndAirTempDataQuery(string table, RoadAndAirTempData data);
        string InsertAverageTempDataQuery(string table, RoadAndAirTempData data, double Radius);
        string SelectAllQuery(string table);
        string SelectWhereQuery(string table, string parameter, string value);
        string UpdateNewestQuery(string table, RoadAndAirTempData data, double Latitude, double Longitude);
        string UpdateAverageQuery(string table, RoadAndAirTempData data, AverageTempData originalData);
        string SelectTimeframeQuery(string table, DateTime low, DateTime high);
        public string SelectByTimestampQuery(string table, DateTime time);
    }
}
