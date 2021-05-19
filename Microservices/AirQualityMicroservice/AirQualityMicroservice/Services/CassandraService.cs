using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cassandra;
using AirQualityMicroservice.Contracts;
using AirQualityMicroservice.Entities;

namespace AirQualityMicroservice.Services
{
    public class CassandraService : ICassandraService
    {
        private readonly IDateService _dateService;

        public CassandraService(IDateService dateService)
        {
            this._dateService = dateService;
        }

        public AirQualityData ConvertCassandraAirQualityRow(Row instance)
        {
            AirQualityData roadInfo = new AirQualityData();
            roadInfo.Timestamp = DateTime.Parse(instance["Timestamp"].ToString());
            roadInfo.StationName = instance["StationName"].ToString();
            roadInfo.Latitude = (double)instance["Latitude"];
            roadInfo.Longitude = (double)instance["Longitude"];
            roadInfo.CO = (double)instance["CO"];
            roadInfo.NMHC = (double)instance["NMHC"];
            roadInfo.Benzene = (double)instance["Benzene"];
            roadInfo.NO2 = (double)instance["NO2"];
            roadInfo.NOx = (double)instance["NOx"];
            roadInfo.RelativeHumidity = (double)instance["RelativeHumidity"];
            return roadInfo;
        }

        public AverageAirQualityData ConvertCassandraAverageRow(Row instance)
        {
            AverageAirQualityData averageInfo = new AverageAirQualityData();
            averageInfo.Timestamp = DateTime.Parse(instance["Timestamp"].ToString());
            averageInfo.Radius = (double)instance["Radius"];
            averageInfo.Latitude = (double)instance["Latitude"];
            averageInfo.Longitude = (double)instance["Longitude"];
            averageInfo.DataCount = (int)instance["DataCount"];
            averageInfo.AverageCO = (double)instance["AverageCO"];
            averageInfo.AverageNMHC = (double)instance["AverageNMHC"];
            averageInfo.AverageBenzene = (double)instance["AverageBenzene"];
            averageInfo.AverageNO2 = (double)instance["AverageNO2"];
            averageInfo.AverageNOx = (double)instance["AverageNOx"];
            averageInfo.AverageRelativeHumidity = (double)instance["AverageRelativeHumidity"];
            return averageInfo;
        }

        public string InsertAirQualityDataQuery(string table, AirQualityData data)
        {
            string command = "insert into " + table + " (\"Timestamp\", \"StationName\", \"Latitude\", \"Longitude\", "
                                                      + "\"CO\", \"NMHC\", \"Benzene\", \"NOx\", \"NO2\", \"RelativeHumidity\")";
            command += "values ('" + _dateService.ConvertDateToString(data.Timestamp) + "', '" + data.StationName + "', " + data.Latitude + ", "
                                   + data.Longitude + ", " + data.CO + ", " + data.NMHC + ", " + data.Benzene + ", " + data.NOx + ", " 
                                   + data.NO2 + ", " + data.RelativeHumidity + ");";
            return command;
        }

        public string InsertAverageAirQualityDataQuery(string table, AirQualityData data, double Radius)
        {
            string command = "insert into " + table + " (\"Timestamp\", \"Radius\", \"Latitude\", \"Longitude\", \"DataCount\", "
                                                      + "\"AverageCO\", \"AverageNMHC\", \"AverageBenzene\", \"AverageNOx\", \"AverageNO2\", \"AverageRelativeHumidity\")";
            command += "values ('" + _dateService.ConvertDateToString(data.Timestamp) + "', " + Radius + ", " + data.Latitude + ", " + data.Longitude + ", " + 1 + ", "
                                   + data.CO + ", " + data.NMHC + ", " + data.Benzene + ", " + data.NOx + ", " + data.NO2 + ", " + data.RelativeHumidity + ");";
            return command;
        }

        public string SelectAllQuery(string table)
        {
            return "select * from " + table + ";";
        }

        public string SelectWhereQuery(string table, string parameter, string value)
        {
            return "select * from " + table + " where \"" + parameter + "\"=" + value + " ALLOW FILTERING";
        }

        public string UpdateNewestQuery(string table, AirQualityData data, double Latitude, double Longitude)
        {
            string command = "update " + table;
            command += " set \"Timestamp\"='" + _dateService.ConvertDateToString(data.Timestamp) + "', \"StationName\"='" + data.StationName 
                    + "',  \"CO\"=" + data.CO + ", \"NMHC\"=" + data.NMHC + ", \"Benzene\"=" + data.Benzene
                    + ", \"NOx\"=" + data.NOx + ", \"NO2\"=" + data.NO2 + ", \"RelativeHumidity\"=" + data.RelativeHumidity;
            command += " where \"Latitude\"=" + Latitude + " AND \"Longitude\"=" + Longitude + ";";
            return command;
        }

        public string UpdateAverageQuery(string table, AirQualityData data, AverageAirQualityData originalData)
        {
            string command = "update " + table;
            command += " set \"DataCount\"=" + (originalData.DataCount + 1)
                    + ", \"Radius\"=" + originalData.Radius
                    + ",  \"AverageCO\"="
                    + (CalculateAverage(originalData.DataCount, originalData.AverageCO, data.CO))
                    + ",  \"AverageNMHC\"="
                    + (CalculateAverage(originalData.DataCount, originalData.AverageNMHC, data.NMHC))
                    + ",  \"AverageBenzene\"="
                    + (CalculateAverage(originalData.DataCount, originalData.AverageBenzene, data.Benzene))
                    + ",  \"AverageNOx\"="
                    + (CalculateAverage(originalData.DataCount, originalData.AverageNOx, data.NOx))
                    + ",  \"AverageNO2\"="
                    + (CalculateAverage(originalData.DataCount, originalData.AverageNO2, data.NO2))
                    + ",  \"AverageRelativeHumidity\"="
                    + (CalculateAverage(originalData.DataCount, originalData.AverageRelativeHumidity, data.RelativeHumidity));
            command += " where \"Timestamp\"='" + _dateService.ConvertDateToString(originalData.Timestamp) + "'"
                    + " AND \"Latitude\"=" + originalData.Latitude + " AND \"Longitude\"=" + originalData.Longitude + ";";
            return command;
        }

        private double CalculateAverage(int DataCount, double OldAverage, double newData)
        {
            return DataCount / (double)(DataCount + 1) * OldAverage + newData / (DataCount + 1);
        }

        public string SelectTimeframeQuery(string table, DateTime low, DateTime high)
        {
            string command = "select * from " + table + " where \"Timestamp\" > '" + _dateService.ConvertDateToString(low)
                           + "' and \"Timestamp\" < '" + _dateService.ConvertDateToString(high) + "' ALLOW FILTERING";
            return command;
        }

        public string SelectByTimestampQuery(string table, DateTime time)
        {
            return "select * from " + table + " where \"Timestamp\"='" + _dateService.ConvertDateToString(time) + "' ALLOW FILTERING";
        }
    }
}
