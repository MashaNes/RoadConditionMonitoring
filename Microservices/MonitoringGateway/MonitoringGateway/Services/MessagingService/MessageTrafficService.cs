using Microsoft.Extensions.Hosting;
using MonitoringGateway.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonitoringGateway.Services.MessagingService
{
    public class MessageTrafficService : BackgroundService
    {
        private readonly INotificationService _notificationService;
        private readonly ISubscriptionDataService _subscriptionDataService;

        public MessageTrafficService(INotificationService notificationService, ISubscriptionDataService subscriptionDataService)
        {
            this._notificationService = notificationService;
            this._subscriptionDataService = subscriptionDataService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(100), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await _notificationService.NotifyTraffic(await _subscriptionDataService.GetAllTrafficData());
                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
            }
        }
    }
}
