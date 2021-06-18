using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using VehicleMonitoringGateway.Contracts;
using VehicleMonitoringGateway.Entities;
using VehicleMonitoringGateway.DTOs;
using System.Text;

namespace VehicleMonitoringGateway.Services
{
    public class VehicleService : IVehicleService
    {
        private string _vehicleController = "api/vehicle-location";
        private string _vehicleEndpoint = "get-by-id";
        private string _gatewayRoadController = "api/monitoring-location";
        private string _gatewayRoadEndpoint = "get-newest";
        private double _radius = 300;

        private readonly IUnitOfWork _unitOfWork;

        public VehicleService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<VehicleLocationData> getInfoForVehicle(int vehicleId)
        {
            VehicleLocationData retValue = null;
            
            HttpResponseMessage response = await _unitOfWork.HttpClient.GetAsync(_unitOfWork.VehicleMicroserviceLocation + "/" + _vehicleController + "/" 
                                                                                 + _vehicleEndpoint + "/" + vehicleId);
            if (response.IsSuccessStatusCode)
            {
                VehicleData vehicleData = await JsonSerializer.DeserializeAsync<VehicleData>(await response.Content.ReadAsStreamAsync());
                if (vehicleData is null)
                    return null;

                retValue = new VehicleLocationData();
                retValue.VehicleData = vehicleData;
                
                LocationRadiusDTO DTO = new LocationRadiusDTO();
                DTO.Latitude = vehicleData.Latitude;
                DTO.Longitude = vehicleData.Longitude;
                DTO.RadiusMeters = _radius;
                response = await _unitOfWork.HttpClient.PostAsync(_unitOfWork.MonitoringGatewayLocation + "/" + _gatewayRoadController + "/" + _gatewayRoadEndpoint + "/",
                                                                                 new StringContent(JsonSerializer.Serialize(DTO), Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                retValue.LocationDataList = await JsonSerializer.DeserializeAsync<List<LocationData>>(await response.Content.ReadAsStreamAsync());
            }
            
            return retValue;
        }
    }
}
