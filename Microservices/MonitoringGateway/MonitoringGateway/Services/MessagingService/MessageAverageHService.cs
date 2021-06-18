using Microsoft.Extensions.Hosting;
using MonitoringGateway.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonitoringGateway.Services.MessagingService
{
    public class MessageAverageHService : BackgroundService
    {
        private readonly INotificationService _notificationService;
        private readonly ISubscriptionDataService _subscriptionDataService;

        public MessageAverageHService(INotificationService notificationService, ISubscriptionDataService subscriptionDataService)
        {
            this._notificationService = notificationService;
            this._subscriptionDataService = subscriptionDataService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(100), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await _notificationService.NotifyAverageH(await _subscriptionDataService.GetAverageH());
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
