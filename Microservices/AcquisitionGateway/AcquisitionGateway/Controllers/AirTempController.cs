using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcquisitionGateway.Contracts;
using AcquisitionGateway.Entities;

namespace AcquisitionGateway.Controllers
{
    [ApiController]
    [Route("api/temp_and_air_quality")]
    public class AirTempController : ControllerBase
    {
        private readonly IAirTempService _airTempService;

        public AirTempController(IAirTempService airTempService)
        {
            _airTempService = airTempService;
        }

        [HttpPost]
        [Route("add-data")]
        public async Task<ActionResult> AddData([FromBody] AirTempData data)
        {
            await _airTempService.AddData(data);
            return Ok();
        }
    }
}
