using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcquisitionGateway.Contracts;
using AcquisitionGateway.Entities;
using Confluent.Kafka;
using System.Text.Json;

namespace AcquisitionGateway.Services
{
    public class TempFService : ITempFService
    {
        private readonly IUnitOfWork _unitOfWork;
        private Dictionary<string, int> Retries = new Dictionary<string, int>();

        public TempFService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<bool> AddData(RoadAndAirTempData newData)
        {
            newData.AirTemperature = convertTempFtoC(newData.AirTemperature);
            newData.RoadTemperature = convertTempFtoC(newData.RoadTemperature);

            string SerializedData = JsonSerializer.Serialize(newData);
            _unitOfWork.KafkaProducer.Produce("Temperature", new Message<Null, string> { Value = SerializedData }, handler);

            return true;
        }

        private double convertTempFtoC(double tempInF)
        {
            return (tempInF - 32) / (double)1.8;
        }

        private void handler(DeliveryReport<Null, string> report)
        {
            if (report.Error.IsError == true)
            {
                if (!Retries.ContainsKey(report.Message.Value))
                {
                    Retries.Add(report.Message.Value, 1);
                    _unitOfWork.KafkaProducer.Produce("Temperature", report.Message, handler);
                }
                else
                {
                    int NumRetries = Retries[report.Message.Value];
                    Retries.Remove(report.Message.Value);
                    if (NumRetries < 10)
                    {
                        Retries.Add(report.Message.Value, NumRetries + 1);
                        _unitOfWork.KafkaProducer.Produce("Temperature", report.Message, handler);
                    }
                }
            }
            else if (Retries.ContainsKey(report.Message.Value))
                Retries.Remove(report.Message.Value);
        }
    }
}
