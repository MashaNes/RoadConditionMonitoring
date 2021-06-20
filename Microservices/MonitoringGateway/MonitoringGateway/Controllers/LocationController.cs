using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Contracts;
using MonitoringGateway.DTOs;
using MonitoringGateway.Entities;
using MonitoringGateway.Entities.Acquisition;

namespace MonitoringGateway.Controllers
{
    [ApiController]
    [Route("api/monitoring-location")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationDataService _locationDataService;

        public LocationController(ILocationDataService locationDataService)
        {
            this._locationDataService = locationDataService;
        }

        [HttpPost]
        [Route("get-newest")]
        public async Task<ActionResult> GetNewest([FromBody]LocationRadiusDTO location)
        {
            List<LocationData> retVal = await _locationDataService.GetNewest(location);
            return Ok(retVal);
        }

        [HttpPost]
        [Route("get-average-h")]
        public async Task<ActionResult> GetAverageH([FromBody] LocationRadiusDTO location)
        {
            List<AverageLocationData> retVal = await _locationDataService.GetAverageH(location);
            return Ok(retVal);
        }

        [HttpPost]
        [Route("get-average-day")]
        public async Task<ActionResult> GetAverageDay([FromBody] LocationRadiusDTO location)
        {
            List<AverageLocationData> retVal = await _locationDataService.GetAverageDay(location);
            return Ok(retVal);
        }

        [HttpPost]
        [Route("get-average")]
        public async Task<ActionResult> GetAverage([FromBody] AverageDTO average)
        {
            List<AverageLocationData> retVal = await _locationDataService.GetAverage(average);
            return Ok(retVal);
        }

        [HttpPost]
        [Route("get-all")]
        public async Task<ActionResult> GetAll([FromBody] LocationRadiusDTO location)
        {
            List<LocationDataList> retVal = await _locationDataService.GetAll(location);
            return Ok(retVal);
        }

        [HttpPost]
        [Route("get-timeframe")]
        public async Task<ActionResult> GetTimeframe([FromBody] LocationTimeDTO locationTIme)
        {
            List<LocationDataList> retVal = await _locationDataService.GetTimeframe(locationTIme);
            return Ok(retVal);
        }

        [HttpPost]
        [Route("get-traffic-data")]
        public async Task<ActionResult> GetTrafficData([FromBody] LocationRadiusDTO location)
        {
            List<LocationTrafficData> retVal = await _locationDataService.GetTrafficData(location);
            return Ok(retVal);
        }
    }
}
