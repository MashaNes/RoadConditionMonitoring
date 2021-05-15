using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcquisitionGateway.Entities;
using AcquisitionGateway.Contracts;
using System.Text.Json;
using Confluent.Kafka;

namespace AcquisitionGateway.Services
{
    public class AirQualityService : IAirQualityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private Dictionary<string, int> Retries = new Dictionary<string, int>();

        public AirQualityService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<bool> AddData(AirQualityData newData)
        {
            string SerializedData = JsonSerializer.Serialize(newData);
            _unitOfWork.KafkaProducer.Produce("AirQuality", new Message<Null, string> { Value = SerializedData }, handler);

            return true;
        }

        private void handler(DeliveryReport<Null, string> report)
        {
            if (report.Error.IsError == true)
            {
                if (!Retries.ContainsKey(report.Message.Value))
                {
                    Retries.Add(report.Message.Value, 1);
                    _unitOfWork.KafkaProducer.Produce("AirQuality", report.Message, handler);
                }
                else
                {
                    int NumRetries = Retries[report.Message.Value];
                    Retries.Remove(report.Message.Value);
                    if (NumRetries < 10)
                    {
                        Retries.Add(report.Message.Value, NumRetries + 1);
                        _unitOfWork.KafkaProducer.Produce("AirQuality", report.Message, handler);
                    }
                }
            }
            else if (Retries.ContainsKey(report.Message.Value))
                Retries.Remove(report.Message.Value);
        }
    }
}
