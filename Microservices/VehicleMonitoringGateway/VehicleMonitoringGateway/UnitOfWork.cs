using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VehicleMonitoringGateway.Contracts;

namespace VehicleMonitoringGateway
{
    public class UnitOfWork : IUnitOfWork
    {
        private HttpClient _httpClient;

        public HttpClient HttpClient
        {
            get
            {
                if (this._httpClient == null)
                {
                    this._httpClient = new HttpClient();
                    this._httpClient.DefaultRequestHeaders.Accept.Clear();
                    this._httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                }

                return this._httpClient;
            }
        }

        private string _vehicleMicroserviceLocation = "http://vehiclelocationmicroservice:80";
        public string VehicleMicroserviceLocation
        {
            get { return this._vehicleMicroserviceLocation; }
        }

        private string _monitoringGatewayLocation = "http://monitoringgateway:80";
        public string MonitoringGatewayLocation
        {
            get { return this._monitoringGatewayLocation; }
        }
    }
}
