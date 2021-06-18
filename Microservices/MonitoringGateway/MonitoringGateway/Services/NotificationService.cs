using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MonitoringGateway.Contracts;
using MonitoringGateway.Entities;
using MonitoringGateway.Services.MessagingService;

namespace MonitoringGateway.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<MessageHub> _hubContext;

        public NotificationService(IUnitOfWork unitOfWork, IHubContext<MessageHub> hubContext)
        {
            this._unitOfWork = unitOfWork;
            this._hubContext = hubContext;
        }

        public async Task NotifyNewest(List<LocationData> data)
        {
            Console.WriteLine("Notify newest clients.");
            await _hubContext.Clients.Group(MessageHub.NewestGroup).SendAsync(MessageHub.NewestMethod, data);
        }

        public async Task NotifyAverageH(List<AverageLocationData> data)
        {
            Console.WriteLine("Notify average per hour clients.");
            await _hubContext.Clients.Group(MessageHub.AverageHGroup).SendAsync(MessageHub.AverageHMethod, data);
        }

        public async Task NotifyAverageDay(List<AverageLocationData> data)
        {
            Console.WriteLine("Notify average per day clients.");
            await _hubContext.Clients.Group(MessageHub.AverageDayGroup).SendAsync(MessageHub.AverageDayMethod, data);
        }
    }
}
