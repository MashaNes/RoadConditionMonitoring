using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Entities;
using MonitoringGateway.Contracts;

namespace MonitoringGateway.Controllers
{
    [ApiController]
    [Route("api/monitoring-subscription")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionDataService _subscriptionDataService;

        public SubscriptionController(ISubscriptionDataService subscriptionDataService)
        {
            this._subscriptionDataService = subscriptionDataService;
        }

        [HttpGet]
        [Route("get-newest")]
        public async Task<ActionResult> GetNewest()
        {
            List<LocationData> retVal = await _subscriptionDataService.GetNewest();
            return Ok(retVal);
        }

        [HttpGet]
        [Route("get-average-per-hour")]
        public async Task<ActionResult> GetAveragePerHour()
        {
            List<AverageLocationData> retVal = await _subscriptionDataService.GetAverageH();
            return Ok(retVal);
        }

        [HttpGet]
        [Route("get-average-per-day")]
        public async Task<ActionResult> GetAveragePerDay()
        {
            List<AverageLocationData> retVal = await _subscriptionDataService.GetAverageDay();
            return Ok(retVal);
        }
    }
}
