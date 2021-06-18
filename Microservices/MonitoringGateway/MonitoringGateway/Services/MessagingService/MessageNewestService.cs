using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MonitoringGateway.Contracts;

namespace MonitoringGateway.Services.MessagingService
{
    public class MessageNewestService : BackgroundService
    {
        private readonly INotificationService _notificationService;
        private readonly ISubscriptionDataService _subscriptionDataService;

        public MessageNewestService(INotificationService notificationService, ISubscriptionDataService subscriptionDataService)
        {
            this._notificationService = notificationService;
            this._subscriptionDataService = subscriptionDataService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(100), stoppingToken);

            while(!stoppingToken.IsCancellationRequested)
            {
                await _notificationService.NotifyNewest(await _subscriptionDataService.GetNewest());
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
