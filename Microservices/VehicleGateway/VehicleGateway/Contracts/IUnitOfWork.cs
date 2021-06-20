using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace VehicleGateway.Contracts
{
    public interface IUnitOfWork
    {
        IProducer<string, string> KafkaProducer { get; }
        string TopicVehicle { get; }
    }
}
