using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleGateway.Entities;
using VehicleGateway.Contracts;

namespace VehicleGateway.Controllers
{
    [ApiController]
    [Route("api/trajectory_MS")]
    public class VehicleMSController : ControllerBase
    {
        private readonly ISpeedMSService _speedMSService;

        public VehicleMSController(ISpeedMSService speedMSService)
        {
            _speedMSService = speedMSService;
        }

        [HttpPost]
        [Route("add-data")]
        public async Task<ActionResult> AddData([FromBody] VehicleData vehicleData)
        {
            await _speedMSService.AddData(vehicleData);
            return Ok();
        }
    }
}
