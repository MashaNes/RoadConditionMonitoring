using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcquisitionGateway.Entities;
using AcquisitionGateway.Contracts;

namespace AcquisitionGateway.Services
{
    public class AirQualityService : IAirQualityService
    {
        public async Task<bool> AddData(AirQualityData newData)
        {
            //send newData to Kafka topic
            return true;
        }
    }
}
