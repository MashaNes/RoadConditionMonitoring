using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleLocationMicroservice.Entities;

namespace VehicleLocationMicroservice.Contracts
{
    public interface ICassandraService
    {
        void AddData(VehicleData data);
        VehicleData GetForId(int id);
        List<VehicleData> GetAll();
    }
}
