using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cassandra;

namespace TemperatureMicroservice.Contracts
{
    public interface IUnitOfWork
    {
        ISession CassandraSession { get; }
        string TableAllData { get; }
        string TableNewestData { get; }
        string TableAverageH { get; }
        string TableAverageDay { get; }
    }
}
