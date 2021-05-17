using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemperatureMicroservice.DTOs
{
    public class LocationRadiusDTO
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double RadiusMeters { get; set; }

        public LocationRadiusDTO()
        { }

        public LocationRadiusDTO(LocationTimeDTO locationTimeDTO)
        {
            Latitude = locationTimeDTO.Latitude;
            Longitude = locationTimeDTO.Longitude;
            RadiusMeters = locationTimeDTO.RadiusMeters;
        }
    }
}
