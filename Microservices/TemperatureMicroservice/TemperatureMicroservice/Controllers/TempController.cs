using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemperatureMicroservice.Contracts;
using TemperatureMicroservice.Entities;
using TemperatureMicroservice.DTOs;

namespace TemperatureMicroservice.Controllers
{
    [ApiController]
    [Route("api/temp_microservice")]
    public class TempController : ControllerBase
    {
        private readonly ITempService _tempService;

        public TempController(ITempService tempService)
        {
            _tempService = tempService;
        }

        [HttpGet]
        [Route("get-newest-data")]
        public async Task<ActionResult> GetNewestData()
        {
            List<RoadAndAirTempData> retVal = await _tempService.GetAllNewest();
            return Ok(retVal);
        }

        [HttpPost]
        [Route("get-data-timeframe")]
        public async Task<ActionResult> GetDataTimeframe([FromBody] TimeframeDTO timeframe)
        {
            List<RoadAndAirTempData> retVal = await _tempService.GetDataByTimeframe(timeframe.ReferentTime, timeframe.Seconds);
            return Ok(retVal);
        }

        [HttpPost]
        [Route("get-data-location/{newest}")]
        public async Task<ActionResult> GetDataLocation([FromBody] LocationRadiusDTO locationRadius, bool newest)
        {
            List<RoadAndAirTempData> retVal = await _tempService.GetDataLocation(locationRadius, newest);
            return Ok(retVal);
        }

        [HttpPost]
        [Route("get-data-location-timeframe")]
        public async Task<ActionResult> GetDataLocationTimeframe([FromBody] LocationTimeDTO locationTime)
        {
            List<RoadAndAirTempData> retVal = await _tempService.GetDataTimeframeLocation(locationTime);
            return Ok(retVal);
        }

        [HttpPost]
        [Route("get-data-average")]
        public async Task<ActionResult> GetAverageData([FromBody] AverageDTO average)
        {
            List<AverageTempData> retVal = await _tempService.GetAverageData(average);
            return Ok(retVal);
        }
    }
}
