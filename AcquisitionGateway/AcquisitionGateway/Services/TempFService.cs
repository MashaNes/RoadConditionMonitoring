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
    public class TempFService : KafkaService, ITempFService
    {
        public TempFService(IUnitOfWork unitOfWork) : base(unitOfWork, "Temperature")
        { }

        public async Task<bool> AddData(RoadAndAirTempData newData)
        {
            newData.AirTemperature = convertTempFtoC(newData.AirTemperature);
            newData.RoadTemperature = convertTempFtoC(newData.RoadTemperature);

            string SerializedData = JsonSerializer.Serialize(newData);
            Produce(SerializedData);

            return true;
        }

        private double convertTempFtoC(double tempInF)
        {
            return (tempInF - 32) / (double)1.8;
        }
    }
}
