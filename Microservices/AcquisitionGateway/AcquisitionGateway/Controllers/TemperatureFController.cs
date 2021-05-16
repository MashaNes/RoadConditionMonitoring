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
    [Route("api/road_air_temp_F")]
    public class TemperatureFController : ControllerBase
    {
        private readonly ITempFService _tempFService;

        public TemperatureFController(ITempFService tempFService)
        {
            _tempFService = tempFService;
        }

        [HttpPost]
        [Route("add-data")]
        public async Task<ActionResult> AddData([FromBody] RoadAndAirTempData roadData)
        {
            await _tempFService.AddData(roadData);
            return Ok();
        }
    }
}
