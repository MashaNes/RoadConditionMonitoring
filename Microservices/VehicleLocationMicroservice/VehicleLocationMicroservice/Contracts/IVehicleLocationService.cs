using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleLocationMicroservice.Entities;

namespace VehicleLocationMicroservice.Contracts
{
    public interface IVehicleLocationService
    {
        Task<VehicleData> GetById(int id);
        Task<List<VehicleData>> GetAll();
    }
}
