using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleGateway.Entities;
using VehicleGateway.Contracts;
using System.Text.Json;

namespace VehicleGateway.Services
{
    public class SpeedMSService : KafkaService, ISpeedMSService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpeedMSService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.TopicVehicle)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<bool> AddData(VehicleData newData)
        {
            newData.VehicleSpeed = convertSpeedToKMH(newData.VehicleSpeed);
            string SerializedData = JsonSerializer.Serialize(newData);
			Console.WriteLine(SerializedData);
            Produce(newData.VehicleId.ToString(), SerializedData);
            return true;
        }

        private double convertSpeedToKMH(double speedInMS)
        {
            return speedInMS * 3.6;
        }
    }
}
