using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cassandra;
using TemperatureMicroservice.Entities;

namespace TemperatureMicroservice.Contracts
{
    public interface ICassandraService
    {
        public static RoadAndAirTempData ConvertCassandraTempRow(Row instance)
        {
            RoadAndAirTempData roadInfo = new RoadAndAirTempData();
            roadInfo.Timestamp = DateTime.Parse(instance["Timestamp"].ToString());
            roadInfo.StationName = instance["StationName"].ToString();
            roadInfo.AirTemperature = (double)instance["AirTemperature"];
            roadInfo.Latitude = (double)instance["Latitude"];
            roadInfo.Longitude = (double)instance["Longitude"];
            roadInfo.RecordId = (int)instance["RecordId"];
            roadInfo.RoadTemperature = (double)instance["RoadTemperature"];
            return roadInfo;
        }

        public static AverageTempData ConvertCassandraAverageRow(Row instance)
        {
            AverageTempData averageInfo = new AverageTempData();
            averageInfo.Timestamp = DateTime.Parse(instance["Timestamp"].ToString());
            averageInfo.Radius = (double)instance["Radius"];
            averageInfo.AverageAirTemperature = (double)instance["AverageAirTemperature"];
            averageInfo.Latitude = (double)instance["Latitude"];
            averageInfo.Longitude = (double)instance["Longitude"];
            averageInfo.DataCount = (int)instance["DataCount"];
            averageInfo.AverageRoadTemperature = (double)instance["AverageRoadTemperature"];
            return averageInfo;
        }

        public static string InsertRoadAndAirTempDataQuery(string table, RoadAndAirTempData data)
        {
            string command = "insert into " + table + " (\"Timestamp\", \"StationName\", \"AirTemperature\", \"Latitude\", \"Longitude\", \"RecordId\", \"RoadTemperature\")";
            command += "values ('" + IDateService.ConvertDateToString(data.Timestamp) + "', '" + data.StationName + "', " + data.AirTemperature + ", " + data.Latitude + ", " +
                    + data.Longitude + ", " + data.RecordId + ", " + data.RoadTemperature + ");";
            return command;
        }

        public static string InsertAverageTempDataQuery(string table, RoadAndAirTempData data, double Radius)
        {
            string command = "insert into " + table + " (\"Timestamp\", \"Radius\", \"AverageAirTemperature\", \"Latitude\", \"Longitude\", \"DataCount\", \"AverageRoadTemperature\")";
            command += "values ('" + IDateService.ConvertDateToString(data.Timestamp) + "', " + Radius + ", " + data.AirTemperature + ", " + data.Latitude + ", " +
                    + data.Longitude + ", " + 1 + ", " + data.RoadTemperature + ");";
            return command;
        }

        public static string SelectAllQuery(string table)
        {
            return "select * from " + table + ";";
        }

        public static string SelectWhereQuery(string table, string parameter, string value)
        {
            return "select * from " + table + " where \"" + parameter + "\"=" + value + " ALLOW FILTERING";
        }

        public static string UpdateNewestQuery(string table, RoadAndAirTempData data, double Latitude, double Longitude)
        {
            string command = "update " + table;
            command += " set \"Timestamp\"='" + IDateService.ConvertDateToString(data.Timestamp) + "', \"RecordId\"=" + data.RecordId
                    + ", \"StationName\"='" + data.StationName + "',  \"AirTemperature\"=" + data.AirTemperature + ", \"RoadTemperature\"=" + data.RoadTemperature;
            command += " where \"Latitude\"=" + Latitude + " AND \"Longitude\"=" + Longitude + ";";
            return command;
        }

        public static string UpdateAverageQuery(string table, RoadAndAirTempData data, AverageTempData originalData)
        {
            string command = "update " + table;
            command += " set \"DataCount\"=" + (originalData.DataCount + 1)
                    + ", \"Radius\"='" + originalData.Radius 
                    + "',  \"AverageAirTemperature\"=" + (originalData.AverageAirTemperature * originalData.DataCount + data.AirTemperature)
                    + ", \"AverageRoadTemperature\"=" + (originalData.AverageRoadTemperature * originalData.DataCount + data.RoadTemperature);
            command += "where \"Timestamp\"='" + IDateService.ConvertDateToString(originalData.Timestamp) + "'"
                    + " AND \"Latitude\"=" + originalData.Latitude + " AND \"Longitude\"=" + originalData.Longitude + ";";
            return command;
        }
    }
}
