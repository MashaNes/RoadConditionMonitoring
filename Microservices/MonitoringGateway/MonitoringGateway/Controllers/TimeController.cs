using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Contracts;
using MonitoringGateway.DTOs;
using MonitoringGateway.Entities;

namespace MonitoringGateway.Controllers
{
    [ApiController]
    [Route("api/monitoring-time")]
    public class TimeController : ControllerBase
    {
        private readonly ITimeDataService _timeDataService;

        public TimeController(ITimeDataService timeDataService)
        {
            this._timeDataService = timeDataService;
        }

        [HttpGet]
        [Route("get-average-h/{year}/{month}/{day}/{hour}")]
        public async Task<ActionResult> GetAverageH(int year, int month, int day, int hour)
        {
            DateTime dateTime = new DateTime(year, month, day).AddHours(hour);
            List<AverageLocationData> retVal = await _timeDataService.GetAverage(dateTime, true);
            return Ok(retVal);
        }

        [HttpGet]
        [Route("get-average-day/{year}/{month}/{day}")]
        public async Task<ActionResult> GetAverageDay(int year, int month, int day)
        {
            DateTime dateTime = new DateTime(year, month, day);
            List<AverageLocationData> retVal = await _timeDataService.GetAverage(dateTime, false);
            return Ok(retVal);
        }

        [HttpPost]
        [Route("get-timeframe")]
        public async Task<ActionResult> GetTimeframe([FromBody] TimeframeDTO time)
        {
            List<LocationDataList> retVal = await _timeDataService.GetTimeframe(time);
            return Ok(retVal);
        }
    }
}
