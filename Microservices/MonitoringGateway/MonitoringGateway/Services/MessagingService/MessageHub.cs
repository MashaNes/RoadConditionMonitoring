using Microsoft.AspNetCore.SignalR;
using MonitoringGateway.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringGateway.Services.MessagingService
{
    public class MessageHub : Hub
    {
        private static string _newestGroup = "newest";
        private static string _averageHGroup = "averageH";
        private static string _averageDayGroup = "averageDay";
        private static string _trafficGroup = "traffic";
        private static string _newestMethod = "new_newest";
        private static string _averageHMethod = "new_averageH";
        private static string _averageDayMethod = "new_averageDay";
        private static string _trafficMethod = "new_traffic";

        public static string NewestGroup { get { return _newestGroup; } }
        public static string AverageHGroup { get { return _averageHGroup; } }
        public static string AverageDayGroup { get { return _averageDayGroup; } }
        public static string TrafficGroup { get { return _trafficGroup; } }
        public static string NewestMethod { get { return _newestMethod; } }
        public static string AverageHMethod { get { return _averageHMethod; } }
        public static string AverageDayMethod { get { return _averageDayMethod; } }
        public static string TrafficMethod { get { return _trafficMethod; } }

        public async Task NotifyNewest(List<LocationData> data)
        {
            await Clients.Group(_newestGroup).SendAsync(_newestMethod, data);
        }

        public async Task NotifyAverageH(List<AverageLocationData> data)
        {
            await Clients.Group(_averageHGroup).SendAsync(_averageHMethod, data);
        }

        public async Task NotifyAverageDay(List<AverageLocationData> data)
        {
            await Clients.Group(_averageDayGroup).SendAsync(_averageDayMethod, data);
        }

        public async Task NotifyTraffic(AllTrafficData data)
        {
            await Clients.Group(_trafficGroup).SendAsync(_trafficMethod, data);
        }

        public async Task<string> JoinNewestGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, _newestGroup);
            return "Joined group " + _newestGroup;
        }

        public async Task<string> JoinAverageHGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, _averageHGroup);
            return "Joined group " + _averageHGroup;
        }

        public async Task<string> JoinAverageDayGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, _averageDayGroup);
            return "Joined group " + _averageDayGroup;
        }

        public async Task<string> JoinTrafficGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, _trafficGroup);
            return "Joined group " + _trafficGroup;
        }

        public async Task<string> LeaveNewestGroup()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, _newestGroup);
            return "Left group " + _newestGroup;
        }

        public async Task<string> LeaveAverageHGroup()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, _averageHGroup);
            return "Left group " + _averageHGroup;
        }

        public async Task<string> LeaveAverageDayGroup()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, _averageDayGroup);
            return "Left group " + _averageDayGroup;
        }

        public async Task<string> LeaveTrafficGroup()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, _trafficGroup);
            return "Left group " + _trafficGroup;
        }
    }
}
