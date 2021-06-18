using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace VehicleMonitoringGateway.Contracts
{
    public interface IUnitOfWork
    {
        HttpClient HttpClient { get; }
        string VehicleMicroserviceLocation { get; }
        string MonitoringGatewayLocation { get; }
    }
}
