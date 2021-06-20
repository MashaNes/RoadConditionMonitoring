using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrafficMicroservice.Contracts;
using TrafficMicroservice.Entities;

namespace TrafficMicroservice.Services
{
    public class CassandraService : ICassandraService
    {
        public GlobalTraffic ConvertCassandraGlobalRow(Row instance)
        {
            GlobalTraffic globalTraffic = new GlobalTraffic();
            globalTraffic.ParamName = instance["paramname"].ToString();
            globalTraffic.Value = (double)instance["value"];
            return globalTraffic;
        }

        public LocationTraffic ConvertCassandraLocationRow(Row instance)
        {
            LocationTraffic locationTraffic = new LocationTraffic();
            locationTraffic.Latitude = (double)instance["latitude"];
            locationTraffic.Longitude = (double)instance["longitude"];
            locationTraffic.ParamName = instance["paramname"].ToString();
            locationTraffic.Value = (double)instance["value"];
            return locationTraffic;
        }

        public string SelectWhereQuery(string table, string parameter, string value)
        {
            return "select * from " + table + " where " + parameter + "='" + value + "' ALLOW FILTERING;";
        }

        public string SelectWhereLocationQuery(string table, double Latitude, double Longitude, string parameter, string value)
        {
            return "select * from " + table + " where " 
                   + "latitude=" + Latitude + " and longitude=" + Longitude + " and "
                   + parameter + "='" + value + "' ALLOW FILTERING;";
        }
    }
}
