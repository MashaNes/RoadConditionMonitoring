using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace AcquisitionGateway.Contracts
{
    public interface IUnitOfWork
    {
        IProducer<Null, string> KafkaProducer { get; }
        string TopicTemperature { get; }
        string TopicAirQuality { get; }
    }
}
