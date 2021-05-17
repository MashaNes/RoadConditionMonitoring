﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cassandra;
using TemperatureMicroservice.Entities;
using TemperatureMicroservice.Contracts;
using Geolocation;

namespace TemperatureMicroservice.Services
{
    public class CassandraService : ICassandraService
    {
        private readonly IDateService _dateService;

        public CassandraService(IDateService dateService)
        {
            this._dateService = dateService;
        }

        public RoadAndAirTempData ConvertCassandraTempRow(Row instance)
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

        public AverageTempData ConvertCassandraAverageRow(Row instance)
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

        public string InsertRoadAndAirTempDataQuery(string table, RoadAndAirTempData data)
        {
            string command = "insert into " + table + " (\"Timestamp\", \"StationName\", \"AirTemperature\", \"Latitude\", \"Longitude\", \"RecordId\", \"RoadTemperature\")";
            command += "values ('" + _dateService.ConvertDateToString(data.Timestamp) + "', '" + data.StationName + "', " + data.AirTemperature + ", " + data.Latitude + ", " +
                    + data.Longitude + ", " + data.RecordId + ", " + data.RoadTemperature + ");";
            return command;
        }

        public string InsertAverageTempDataQuery(string table, RoadAndAirTempData data, double Radius)
        {
            string command = "insert into " + table + " (\"Timestamp\", \"Radius\", \"AverageAirTemperature\", \"Latitude\", \"Longitude\", \"DataCount\", \"AverageRoadTemperature\")";
            command += "values ('" + _dateService.ConvertDateToString(data.Timestamp) + "', " + Radius + ", " + data.AirTemperature + ", " + data.Latitude + ", " +
                    + data.Longitude + ", " + 1 + ", " + data.RoadTemperature + ");";
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

        public string UpdateNewestQuery(string table, RoadAndAirTempData data, double Latitude, double Longitude)
        {
            string command = "update " + table;
            command += " set \"Timestamp\"='" + _dateService.ConvertDateToString(data.Timestamp) + "', \"RecordId\"=" + data.RecordId
                    + ", \"StationName\"='" + data.StationName + "',  \"AirTemperature\"=" + data.AirTemperature + ", \"RoadTemperature\"=" + data.RoadTemperature;
            command += " where \"Latitude\"=" + Latitude + " AND \"Longitude\"=" + Longitude + ";";
            return command;
        }

        public string UpdateAverageQuery(string table, RoadAndAirTempData data, AverageTempData originalData)
        {
            string command = "update " + table;
            command += " set \"DataCount\"=" + (originalData.DataCount + 1)
                    + ", \"Radius\"='" + originalData.Radius
                    + "',  \"AverageAirTemperature\"="
                    + (originalData.DataCount / (originalData.DataCount + 1) * originalData.AverageAirTemperature + data.AirTemperature / (originalData.DataCount + 1))
                    + ", \"AverageRoadTemperature\"="
                    + (originalData.DataCount / (originalData.DataCount + 1) * originalData.AverageRoadTemperature + data.RoadTemperature / (originalData.DataCount + 1));
            command += "where \"Timestamp\"='" + _dateService.ConvertDateToString(originalData.Timestamp) + "'"
                    + " AND \"Latitude\"=" + originalData.Latitude + " AND \"Longitude\"=" + originalData.Longitude + ";";
            return command;
        }

        public string SelectTimeframeQuery(string table, DateTime low, DateTime high)
        {
            string command = "select * from " + table + " where \"Timestamp\" > '" + _dateService.ConvertDateToString(low)
                           + "' and \"Timestamp\" < '" + _dateService.ConvertDateToString(high) + "' ALLOW FILTERING";
            return command;
        }

        public string SelectByTimestampQuery(string table, DateTime time)
        {
            return "select * ftom " + table + " where \"Timestamp\"='" + _dateService.ConvertDateToString(time) + "' ALLOW FILTERING";
        }
    }
}
