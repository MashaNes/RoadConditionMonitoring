using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcquisitionGateway.Contracts;
using AcquisitionGateway.Entities;

namespace AcquisitionGateway.Services
{
    public class TempFService : ITempFService
    {
        public async Task<bool> AddData(RoadAndAirTempData newData)
        {
            newData.AirTemperature = convertTempFtoC(newData.AirTemperature);
            newData.RoadTemperature = convertTempFtoC(newData.RoadTemperature);
            //send newData to temperature Kafka topic
            return true;
        }

        private double convertTempFtoC(double tempInF)
        {
            return (tempInF - 32) / (double)1.8;
        }
    }
}
