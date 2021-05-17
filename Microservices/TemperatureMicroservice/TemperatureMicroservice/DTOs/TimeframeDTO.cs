using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemperatureMicroservice.DTOs
{
    public class TimeframeDTO
    {
        public int Seconds { get; set; }
        public DateTime ReferentTime { get; set; }
    }
}
