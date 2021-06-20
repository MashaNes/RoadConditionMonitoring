using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrafficMicroservice.Contracts;

namespace TrafficMicroservice
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
                    Cluster cluster = Cluster.Builder().AddContactPoint("cassandra-traffic").Build();
                    this._cassandraSession = cluster.Connect("traffic_data");
                }

                return _cassandraSession;
            }
        }

        private string _tableGeneral = "general_traffic";
        public string TableGeneral
        {
            get 
            {
                return this._tableGeneral;
            }
        }

        private string _tableLocation = "location_traffic";
        public string TableLocation
        {
            get
            {
                return _tableLocation;
            }
        }
    }
}
