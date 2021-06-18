using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleMonitoringGateway.Contracts;
using VehicleMonitoringGateway.Entities;

namespace VehicleMonitoringGateway.Controllers
{
    [ApiController]
    [Route("api/vehicle")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            this._vehicleService = vehicleService;
        }

        [HttpGet]
        [Route("get-info/{vehicleId}/{radius}")]
        public async Task<ActionResult> GetInfo(int vehicleId, double radius)
        {
            VehicleLocationData result = await _vehicleService.getInfoForVehicle(vehicleId, radius);
            if (result == null)
                return BadRequest("There is currently no available data for that vehicle.");
            return Ok(result);
        }
    }
}
