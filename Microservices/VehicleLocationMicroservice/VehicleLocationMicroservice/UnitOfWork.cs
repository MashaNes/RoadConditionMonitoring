using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Cassandra;
using VehicleLocationMicroservice.Contracts;

namespace VehicleLocationMicroservice
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
                    Cluster cluster = Cluster.Builder().AddContactPoint("cassandra2").Build();
                    this._cassandraSession = cluster.Connect("vehicle_location_data");
                }

                return _cassandraSession;
            }
        }

        private IConsumer<string, string> _kafkaConsumer;

        public IConsumer<string, string> KafkaConsumer
        {
            get
            {
                if (this._kafkaConsumer == null)
                {
                    var config = new ConsumerConfig
                    {
                        BootstrapServers = "kafka:29092",
                        GroupId = "VehicleLocation",
                        AutoOffsetReset = AutoOffsetReset.Earliest,
                        EnableAutoCommit = true,
                        EnableAutoOffsetStore = false
                    };

                    _kafkaConsumer = new ConsumerBuilder<string, string>(config).Build();
                }

                return _kafkaConsumer;
            }
        }

        private string _kafkaTopic = "VehicleLocation";

        public string KafkaTopic
        {
            get
            {
                return this._kafkaTopic;
            }
        }

        private string _tableData = "vehicle_location";
        public string TableData
        {
            get
            {
                return this._tableData;
            }
        }
    }
}
