using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleLocationMicroservice.Contracts;
using VehicleLocationMicroservice.Entities;

namespace VehicleLocationMicroservice.Services
{
    public class VehicleLocationService : IVehicleLocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICassandraService _cassandraService;

        public VehicleLocationService(IUnitOfWork unitOfWork, ICassandraService cassandraService)
        {
            this._unitOfWork = unitOfWork;
            this._cassandraService = cassandraService;
        }

        public async Task<VehicleData> GetById(int id)
        {
            return _cassandraService.GetForId(id);
        }

        public async Task<List<VehicleData>> GetAll()
        {
            return _cassandraService.GetAll();
        }
    }
}
