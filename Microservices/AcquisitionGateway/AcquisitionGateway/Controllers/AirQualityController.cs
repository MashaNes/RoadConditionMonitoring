using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcquisitionGateway.Entities;
using AcquisitionGateway.Contracts;

namespace AcquisitionGateway.Controllers
{
    [ApiController]
    [Route("api/air_quality")]
    public class AirQualityController : ControllerBase
    {
        private readonly IAirQualityService _airQualityService;

        public AirQualityController(IAirQualityService airQualityService)
        {
            _airQualityService = airQualityService;
        }

        [HttpPost]
        [Route("add-data")]
        public async Task<ActionResult> AddData([FromBody] AirQualityData airData)
        {
            await _airQualityService.AddData(airData);
            return Ok();
        }
    }
}
