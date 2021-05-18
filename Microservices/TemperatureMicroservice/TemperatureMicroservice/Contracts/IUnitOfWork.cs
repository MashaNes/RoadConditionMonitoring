using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cassandra;
using Confluent.Kafka;

namespace TemperatureMicroservice.Contracts
{
    public interface IUnitOfWork
    {
        ISession CassandraSession { get; }
        IConsumer<Null, string> KafkaConsumer { get; }
        string KafkaTopic { get; }
        string TableAllData { get; }
        string TableNewestData { get; }
        string TableAverageH { get; }
        string TableAverageDay { get; }
    }
}
