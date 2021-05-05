﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcquisitionGateway.Entities
{
    public class RoadAndAirTempData
    {
        public int RecordId { get; set; }
        public String StationName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
        public double AirTemperature { get; set; }
        public double RoadTemperature { get; set; }
    }
}
