using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace MonitoringGateway.Contracts
{
    public interface IUnitOfWork
    {
        HttpClient HttpClient { get; }
        String Host { get; }
    }
}
