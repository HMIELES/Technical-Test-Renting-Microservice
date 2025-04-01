using System;

namespace Renting.Application.DTOs
{
    public class CreateVehicleResponse
    {
        public Guid VehicleId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime ManufactureDate { get; set; }

        public CreateVehicleResponse(Guid vehicleId, string brand, string model, DateTime manufactureDate)
        {
            VehicleId = vehicleId;
            Brand = brand;
            Model = model;
            ManufactureDate = manufactureDate;
        }
    }
}

