using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringGateway.DTOs
{
    public class AverageDTO
    {
        public DateTime Timestamp { get; set; }
        public bool PerHour { get; set; }
        public LocationRadiusDTO LocationInfo { get; set; }
    }
}
