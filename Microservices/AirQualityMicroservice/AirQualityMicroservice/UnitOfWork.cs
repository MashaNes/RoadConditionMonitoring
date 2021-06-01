using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cassandra;
using Confluent.Kafka;
using AirQualityMicroservice.Contracts;

namespace AirQualityMicroservice
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
                    Cluster cluster = Cluster.Builder().AddContactPoint("cassandra-air").Build();
                    this._cassandraSession = cluster.Connect("air_quality_data");
                }

                return _cassandraSession;
            }
        }

        private IConsumer<Null, string> _kafkaConsumer;

        public IConsumer<Null, string> KafkaConsumer
        {
            get
            {
                if (this._kafkaConsumer == null)
                {
                    var config = new ConsumerConfig
                    {
                        BootstrapServers = "kafka:29092",
                        GroupId = "AirQuality",
                        AutoOffsetReset = AutoOffsetReset.Earliest,
                        EnableAutoCommit = true,
                        EnableAutoOffsetStore = false
                    };

                    _kafkaConsumer = new ConsumerBuilder<Null, string>(config).Build();
                }

                return _kafkaConsumer;
            }
        }

        private string _kafkaTopic = "AirQuality";

        public string KafkaTopic
        {
            get
            {
                return this._kafkaTopic;
            }
        }

        private string _tableAllData = "all_air_quality";
        public string TableAllData
        {
            get
            {
                return this._tableAllData;
            }
        }

        private string _tableNewestData = "newest_air_quality";
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
