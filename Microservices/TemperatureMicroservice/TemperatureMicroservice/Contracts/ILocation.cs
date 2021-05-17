using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemperatureMicroservice.Contracts
{
    public interface ILocation
    {
        double Latitude { get; }
        double Longitude { get; }
    }
}
