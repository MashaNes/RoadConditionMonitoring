using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleLocationMicroservice.Contracts;
using VehicleLocationMicroservice.Entities;

namespace VehicleLocationMicroservice.Controllers
{
    [ApiController]
    [Route("api/vehicle-location")]
    public class VehicleLocationController : ControllerBase
    {
        private readonly IVehicleLocationService _vehicleLocationService;

        public VehicleLocationController(IVehicleLocationService vehicleLocationService)
        {
            _vehicleLocationService = vehicleLocationService;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _vehicleLocationService.GetAll());
        }

        [HttpGet]
        [Route("get-by-id/{vehicleId}")]
        public async Task<ActionResult> GetById(int vehicleId)
        {
            VehicleData result = await _vehicleLocationService.GetById(vehicleId);
            if (result is null)
                return BadRequest("No data for that vehicle.");
            return Ok(await _vehicleLocationService.GetById(vehicleId));
        }
    }
}
