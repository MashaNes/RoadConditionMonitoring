using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MonitoringGateway.Contracts;
using MonitoringGateway.DTOs;
using MonitoringGateway.Entities;
using MonitoringGateway.Entities.Acquisition;

namespace MonitoringGateway.Services
{
    public class TrafficService : ITrafficService
    {
        private readonly string serviceLocation = "http://192.168.0.26:49155";
        private readonly string controller = "api/traffic_microservice";
        private readonly string globalEndpoint = "get-global-data";
        private readonly string locationAllEndpoint = "get-available-location-data";
        private readonly string locationNearEndpoint = "get-near-location-data";

        private readonly IUnitOfWork _unitOfWork;

        public TrafficService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<AllTrafficData> GetAllTrafficData()
        {
            AllTrafficData allTrafficData = new AllTrafficData();

            HttpResponseMessage response = await _unitOfWork.HttpClient.GetAsync(serviceLocation + "/" + controller + "/" + globalEndpoint);
            allTrafficData.GlobalTrafficData = await JsonSerializer.DeserializeAsync<TrafficData>(await response.Content.ReadAsStreamAsync());

            response = await _unitOfWork.HttpClient.GetAsync(serviceLocation + "/" + controller + "/" + locationAllEndpoint);
            allTrafficData.LocationTrafficData = await JsonSerializer.DeserializeAsync<List<LocationTrafficData>>(await response.Content.ReadAsStreamAsync());

            return allTrafficData;
        }

        public async Task<List<LocationTrafficData>> GetLocationTrafficData(LocationRadiusDTO locationInfo)
        {
            HttpResponseMessage response = await _unitOfWork.HttpClient.PostAsync(serviceLocation + "/" + controller + "/" + locationNearEndpoint + "/",
                                                                                  new StringContent(JsonSerializer.Serialize(locationInfo), Encoding.UTF8, "application/json"));
            return await JsonSerializer.DeserializeAsync<List<LocationTrafficData>>(await response.Content.ReadAsStreamAsync());
        }
    }
}
