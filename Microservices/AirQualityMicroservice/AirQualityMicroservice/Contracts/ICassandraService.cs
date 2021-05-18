using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirQualityMicroservice.Entities;
using Cassandra;

namespace AirQualityMicroservice.Contracts
{
    public interface ICassandraService
    {
        AirQualityData ConvertCassandraAirQualityRow(Row instance);
        AverageAirQualityData ConvertCassandraAverageRow(Row instance);
        string InsertAirQualityDataQuery(string table, AirQualityData data);
        string InsertAverageAirQualityDataQuery(string table, AirQualityData data, double Radius);
        string SelectAllQuery(string table);
        string SelectWhereQuery(string table, string parameter, string value);
        string UpdateNewestQuery(string table, AirQualityData data, double Latitude, double Longitude);
        string UpdateAverageQuery(string table, AirQualityData data, AverageAirQualityData originalData);
        string SelectTimeframeQuery(string table, DateTime low, DateTime high);
        public string SelectByTimestampQuery(string table, DateTime time);
    }
}
