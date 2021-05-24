using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using AcquisitionGateway.Contracts;

namespace AcquisitionGateway
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProducer<Null, string> _kafkaProducer;

        public IProducer<Null, string> KafkaProducer
        {
            get
            {
                if(this._kafkaProducer == null)
                {
                    var config = new ProducerConfig
                    {
                        BootstrapServers = "192.168.0.26:29092",
                        MessageSendMaxRetries = 10,
                        RetryBackoffMs = 100,
                        LingerMs = 100
                    };

                    this._kafkaProducer = new ProducerBuilder<Null, string>(config).Build();
                }

                return _kafkaProducer;
            }
        }

        private string _topicTemperature = "Temperature";

        public string TopicTemperature
        {
            get
            {
                return this._topicTemperature;
            }
        }

        private string _topicAirQuality = "AirQuality";

        public string TopicAirQuality
        {
            get
            {
                return this._topicAirQuality;
            }
        }

        ~UnitOfWork()
        {
            KafkaProducer.Dispose();
        }
    }
}
