using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcquisitionGateway.Contracts;
using Confluent.Kafka;

namespace AcquisitionGateway.Services
{
    public class KafkaService : IKafkaService
    {
        private string Topic;
        private readonly IUnitOfWork _unitOfWork;
        private Dictionary<string, int> Retries = new Dictionary<string, int>();

        public KafkaService(IUnitOfWork unitOfWork, string Topic)
        {
            this._unitOfWork = unitOfWork;
            this.Topic = Topic;
        }

        ~KafkaService()
        {
            _unitOfWork.KafkaProducer.Flush();
        }

        public void Produce(string Data)
        {
            _unitOfWork.KafkaProducer.Produce(Topic, new Message<Null, string> { Value = Data}, handler);
        }

        private void handler(DeliveryReport<Null, string> report)
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
