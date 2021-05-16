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
                        BootstrapServers = "192.168.0.26:29092"
                    };

                    this._kafkaProducer = new ProducerBuilder<Null, string>(config).Build();
                }

                return _kafkaProducer;
            }
        }
    }
}
