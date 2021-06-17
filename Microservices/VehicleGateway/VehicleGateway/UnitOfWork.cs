using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using VehicleGateway.Contracts;

namespace VehicleGateway
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProducer<Null, string> _kafkaProducer;

        public IProducer<Null, string> KafkaProducer
        {
            get
            {
                if (this._kafkaProducer == null)
                {
                    var config = new ProducerConfig
                    {
                        BootstrapServers = "kafka:29092",
                        MessageSendMaxRetries = 10,
                        RetryBackoffMs = 100,
                        LingerMs = 100
                    };

                    this._kafkaProducer = new ProducerBuilder<Null, string>(config).Build();
                }

                return _kafkaProducer;
            }
        }

        private string _topicVehicle = "VehicleLocation";

        public string TopicVehicle
        {
            get
            {
                return this._topicVehicle;
            }
        }

        ~UnitOfWork()
        {
            KafkaProducer.Dispose();
        }
    }
}
