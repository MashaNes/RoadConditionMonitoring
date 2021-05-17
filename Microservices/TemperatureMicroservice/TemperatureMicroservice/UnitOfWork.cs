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

        private string _tableAllData = "all_temperatures";
        public string TableAllData
        {
            get
            {
                return this._tableAllData;
            }
        }

        private string _tableNewestData = "newest_temperatures";
        public string TableNewestData
        {
            get
            {
                return this._tableNewestData;
            }
        }

        private string _tableAverageH = "average_per_h";
        public string TableAverageH
        {
            get
            {
                return this._tableAverageH;
            }
        }

        private string _tableAverageDay = "average_per_day";
        public string TableAverageDay
        {
            get
            {
                return this._tableAverageDay;
            }
        }
    }
}
