using Microsoft.Extensions.Hosting;
using MonitoringGateway.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonitoringGateway.Services.MessagingService
{
    public class MessageAverageDayService : BackgroundService
    {
        private readonly INotificationService _notificationService;
        private readonly ISubscriptionDataService _subscriptionDataService;

        public MessageAverageDayService(INotificationService notificationService, ISubscriptionDataService subscriptionDataService)
        {
            this._notificationService = notificationService;
            this._subscriptionDataService = subscriptionDataService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(100), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await _notificationService.NotifyAverageDay(await _subscriptionDataService.GetAverageDay());
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
