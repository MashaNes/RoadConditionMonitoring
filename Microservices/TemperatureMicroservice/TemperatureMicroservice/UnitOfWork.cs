using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cassandra;
using TemperatureMicroservice.Contracts;

namespace TemperatureMicroservice
{
    public class UnitOfWork : IUnitOfWork
    {
        private ISession _cassandraSession;
        public ISession CassandraSession
        {
            get
            {
                if (this._cassandraSession == null)
                {
                    Cluster cluster = Cluster.Builder().AddContactPoint("192.168.0.26").WithPort(9043).Build();
                    this._cassandraSession = cluster.Connect("temperature_data");
                }

                return _cassandraSession;
            }
        }
    }
}
