using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleGateway.Contracts;

namespace VehicleGateway.Services
{
    public class KafkaService
    {
        private string Topic;
        private readonly IUnitOfWork _unitOfWork;
        private Dictionary<string, int> Retries = new Dictionary<string, int>();

        public KafkaService(IUnitOfWork unitOfWork, string Topic)
        {
            this._unitOfWork = unitOfWork;
            this.Topic = Topic;
        }

        public void Produce(string Key, string Data)
        {
			Console.WriteLine("Producing message");
            _unitOfWork.KafkaProducer.Produce(Topic, new Message<string, string> { Key = Key, Value = Data }, handler);
			Console.WriteLine("Message produced");
        }

        private void handler(DeliveryReport<string, string> report)
        {
            if (report.Error.IsError == true)
            {
                if (!Retries.ContainsKey(report.Message.Value))
                {
                    Retries.Add(report.Message.Value, 1);
                    _unitOfWork.KafkaProducer.Produce(Topic, report.Message, handler);
                }
                else
                {
                    int NumRetries = Retries[report.Message.Value];
                    Retries.Remove(report.Message.Value);
                    if (NumRetries < 10)
                    {
                        Retries.Add(report.Message.Value, NumRetries + 1);
                        _unitOfWork.KafkaProducer.Produce(Topic, report.Message, handler);
                    }
                }
            }
            else if (Retries.ContainsKey(report.Message.Value))
                Retries.Remove(report.Message.Value);
        }
    }
}
