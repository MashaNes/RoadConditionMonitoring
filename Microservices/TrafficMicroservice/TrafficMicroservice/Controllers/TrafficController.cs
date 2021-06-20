using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrafficMicroservice.Contracts;
using TrafficMicroservice.DTOs;

namespace TrafficMicroservice.Controllers
{
    [ApiController]
    [Route("api/traffic_microservice")]
    public class TrafficController : ControllerBase
    {
        private readonly ITrafficService _trafficService;

        public TrafficController(ITrafficService trafficService)
        {
            this._trafficService = trafficService;
        }

        [HttpGet]
        [Route("get-global-data")]
        public async Task<ActionResult> GetGlobal()
        {
            return Ok(this._trafficService.GetGlobalData());
        }

        [HttpGet]
        [Route("get-available-location-data")]
        public async Task<ActionResult> GetAvailableLocation()
        {
            return Ok(this._trafficService.GetAllAvailableLocationData());
        }

        [HttpPost]
        [Route("get-near-location-data")]
        public async Task<ActionResult> GetNearLocation([FromBody]LocationRadiusDTO locationInfo)
        {
            return Ok(this._trafficService.GetNearAvailableLocationData(locationInfo.Latitude, locationInfo.Longitude, locationInfo.RadiusMeters));
        }
    }
}
