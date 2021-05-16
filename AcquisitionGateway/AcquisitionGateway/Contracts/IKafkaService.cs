using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcquisitionGateway.Contracts
{
    public interface IKafkaService
    {
        void Produce(string Data);
    }
}
