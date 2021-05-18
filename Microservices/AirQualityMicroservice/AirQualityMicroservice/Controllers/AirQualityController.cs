using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirQualityMicroservice.Contracts;
using AirQualityMicroservice.Entities;
using AirQualityMicroservice.DTOs;

namespace AirQualityMicroservice.Controllers
{
    [ApiController]
    [Route("api/air_quality_microservice")]
    public class AirQualityController : ControllerBase
    {
        private readonly IAirQualityService _airQualityService;

        public AirQualityController(IAirQualityService airQualityService)
        {
            this._airQualityService = airQualityService;
        }

        [HttpGet]
        [Route("get-newest-data")]
        public async Task<ActionResult> GetNewestData()
        {
            List<AirQualityData> retVal = await _airQualityService.GetAllNewest();
            return Ok(retVal);
        }

        [HttpPost]
        [Route("get-data-timeframe")]
        public async Task<ActionResult> GetDataTimeframe([FromBody] TimeframeDTO timeframe)
        {
            List<AirQualityData> retVal = await _airQualityService.GetDataByTimeframe(timeframe.ReferentTime, timeframe.Seconds);
            return Ok(retVal);
        }

        [HttpPost]
        [Route("get-data-location/{newest}")]
        public async Task<ActionResult> GetDataLocation([FromBody] LocationRadiusDTO locationRadius, bool newest)
        {
            List<AirQualityData> retVal = await _airQualityService.GetDataLocation(locationRadius, newest);
            return Ok(retVal);
        }

        [HttpPost]
        [Route("get-data-location-timeframe")]
        public async Task<ActionResult> GetDataLocationTimeframe([FromBody] LocationTimeDTO locationTime)
        {
            List<AirQualityData> retVal = await _airQualityService.GetDataTimeframeLocation(locationTime);
            return Ok(retVal);
        }

        [HttpPost]
        [Route("get-data-average")]
        public async Task<ActionResult> GetAverageData([FromBody] AverageDTO average)
        {
            List<AverageAirQualityData> retVal = await _airQualityService.GetAverageData(average);
            return Ok(retVal);
        }
    }
}
