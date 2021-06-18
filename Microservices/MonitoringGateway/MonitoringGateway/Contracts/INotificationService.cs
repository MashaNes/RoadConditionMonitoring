using MonitoringGateway.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringGateway.Contracts
{
    public interface INotificationService
    {
        Task NotifyNewest(List<LocationData> data);
        Task NotifyAverageH(List<AverageLocationData> data);
        Task NotifyAverageDay(List<AverageLocationData> data);
    }
}
