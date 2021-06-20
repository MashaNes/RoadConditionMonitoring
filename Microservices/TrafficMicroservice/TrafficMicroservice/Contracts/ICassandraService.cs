using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrafficMicroservice.Entities;

namespace TrafficMicroservice.Contracts
{
    public interface ICassandraService
    {
        GlobalTraffic ConvertCassandraGlobalRow(Row instance);
        LocationTraffic ConvertCassandraLocationRow(Row instance);
        string SelectWhereQuery(string table, string parameter, string value);
        string SelectWhereLocationQuery(string table, double Latitude, double Longitude, string parameter, string value);
    }
}
