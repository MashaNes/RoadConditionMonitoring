using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcquisitionGateway.Contracts;
using AcquisitionGateway.Entities;

namespace AcquisitionGateway.Services
{
    public class AirTempService : IAirTempService
    {
        private readonly IAirQualityService _airQualityService;
        private readonly ITempFService _tempFService;

        public AirTempService(IAirQualityService airQualityService, ITempFService tempFService)
        {
            _airQualityService = airQualityService;
            _tempFService = tempFService;
        }

        public async Task AddData(AirTempData data)
        {
            AirQualityData airQualityData = new AirQualityData(data);
            RoadAndAirTempData roadAndAirTempData = new RoadAndAirTempData(data);

            await _airQualityService.AddData(airQualityData);
            await _tempFService.AddData(roadAndAirTempData);
        }
    }
}
