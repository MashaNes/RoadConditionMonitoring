using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringGateway.Contracts;
using MonitoringGateway.Entities.Acquisition;
using System.Text.Json;
using System.Net.Http;
using MonitoringGateway.DTOs;
using System.Text;

namespace MonitoringGateway.Services
{
    public class AirQualityService : IAirQualityService
    {
        private readonly string serviceLocation = "http://192.168.0.26:49162";
        private readonly string controller = "api/air_quality_microservice";
        private readonly string newestEndpoint = "get-newest-data";
        private readonly string averageEndpoint = "get-data-average";
        private readonly string locationEndpoint = "get-data-location";
        private readonly string timeframeEndpoint = "get-data-timeframe";
        private readonly string locationTimeframeEndpoint = "get-data-location-timeframe";

        private readonly IUnitOfWork _unitOfWork;

        public AirQualityService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<List<AirQualityData>> GetNewest()
        {
            HttpResponseMessage response = await _unitOfWork.HttpClient.GetAsync(serviceLocation + "/" + controller + "/" + newestEndpoint + "/");
            if (response.IsSuccessStatusCode)
                return await JsonSerializer.DeserializeAsync<List<AirQualityData>>(await response.Content.ReadAsStreamAsync());
            return null;
        }

        public async Task<List<AverageAirQualityData>> GetAveragePerH()
        {
            AverageDTO dto = new AverageDTO();
            dto.LocationInfo = null;
            dto.PerHour = true;
            dto.Timestamp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddHours(DateTime.Now.Hour);

            HttpResponseMessage response = await _unitOfWork.HttpClient.PostAsync(serviceLocation + "/" + controller + "/" + averageEndpoint + "/",
                                                                                  new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<List<AverageAirQualityData>>(await response.Content.ReadAsStreamAsync());
        }

        public async Task<List<AverageAirQualityData>> GetAveragePerDay()
        {
            AverageDTO dto = new AverageDTO();
            dto.LocationInfo = null;
            dto.PerHour = false;
            dto.Timestamp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            HttpResponseMessage response = await _unitOfWork.HttpClient.PostAsync(serviceLocation + "/" + controller + "/" + averageEndpoint + "/",
                                                                                  new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<List<AverageAirQualityData>>(await response.Content.ReadAsStreamAsync());
        }

        public async Task<List<AirQualityData>> GetNewestLocation(LocationRadiusDTO locationInfo)
        {
            HttpResponseMessage response = await _unitOfWork.HttpClient.PostAsync(serviceLocation + "/" + controller + "/" + locationEndpoint + "/true/",
                                                                                  new StringContent(JsonSerializer.Serialize(locationInfo), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<List<AirQualityData>>(await response.Content.ReadAsStreamAsync());
        }

        public async Task<List<AverageAirQualityData>> GetAverageLocationHour(LocationRadiusDTO locationInfo)
        {
            AverageDTO dto = new AverageDTO();
            dto.LocationInfo = locationInfo;
            dto.PerHour = true;
            dto.Timestamp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddHours(DateTime.Now.Hour);

            HttpResponseMessage response = await _unitOfWork.HttpClient.PostAsync(serviceLocation + "/" + controller + "/" + averageEndpoint + "/",
                                                                                  new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<List<AverageAirQualityData>>(await response.Content.ReadAsStreamAsync());
        }

        public async Task<List<AverageAirQualityData>> GetAverageLocationDay(LocationRadiusDTO locationInfo)
        {
            AverageDTO dto = new AverageDTO();
            dto.LocationInfo = locationInfo;
            dto.PerHour = false;
            dto.Timestamp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            HttpResponseMessage response = await _unitOfWork.HttpClient.PostAsync(serviceLocation + "/" + controller + "/" + averageEndpoint + "/",
                                                                                  new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<List<AverageAirQualityData>>(await response.Content.ReadAsStreamAsync());
        }

        public async Task<List<AverageAirQualityData>> GetAverageLocation(AverageDTO averageInfo)
        {
            HttpResponseMessage response = await _unitOfWork.HttpClient.PostAsync(serviceLocation + "/" + controller + "/" + averageEndpoint + "/",
                                                                                  new StringContent(JsonSerializer.Serialize(averageInfo), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<List<AverageAirQualityData>>(await response.Content.ReadAsStreamAsync());
        }

        public async Task<List<AirQualityData>> GetAllLocation(LocationRadiusDTO locationInfo)
        {
            HttpResponseMessage response = await _unitOfWork.HttpClient.PostAsync(serviceLocation + "/" + controller + "/" + locationEndpoint + "/false/",
                                                                                  new StringContent(JsonSerializer.Serialize(locationInfo), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<List<AirQualityData>>(await response.Content.ReadAsStreamAsync());
        }

        public async Task<List<AirQualityData>> GetTimeframeLocation(LocationTimeDTO locationTimeInfo)
        {
            HttpResponseMessage response = await _unitOfWork.HttpClient.PostAsync(serviceLocation + "/" + controller + "/" + locationTimeframeEndpoint + "/",
                                                                                  new StringContent(JsonSerializer.Serialize(locationTimeInfo), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<List<AirQualityData>>(await response.Content.ReadAsStreamAsync());
        }

        public async Task<List<AverageAirQualityData>> GetAverageDate(DateTime dateTime, bool perHour)
        {
            AverageDTO dto = new AverageDTO();
            dto.LocationInfo = null;
            dto.PerHour = perHour;
            dto.Timestamp = dateTime;

            HttpResponseMessage response = await _unitOfWork.HttpClient.PostAsync(serviceLocation + "/" + controller + "/" + averageEndpoint + "/",
                                                                                  new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<List<AverageAirQualityData>>(await response.Content.ReadAsStreamAsync());
        }

        public async Task<List<AirQualityData>> GetTimeframe(TimeframeDTO timeInfo)
        {
            HttpResponseMessage response = await _unitOfWork.HttpClient.PostAsync(serviceLocation + "/" + controller + "/" + timeframeEndpoint + "/",
                                                                                  new StringContent(JsonSerializer.Serialize(timeInfo), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<List<AirQualityData>>(await response.Content.ReadAsStreamAsync());
        }
    }
}
