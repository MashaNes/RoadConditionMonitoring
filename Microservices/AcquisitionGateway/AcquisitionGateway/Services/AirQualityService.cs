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
    public class AirQualityService : KafkaService, IAirQualityService
    {
        public AirQualityService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.TopicAirQuality)
        { }

        public async Task<bool> AddData(AirQualityData newData)
        {
            string SerializedData = JsonSerializer.Serialize(newData);
            Produce(SerializedData);

            return true;
        }
    }
}
