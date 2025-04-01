using System;

namespace Renting.WebApi.ViewModels
{
    public class VehicleViewModel
    {
        public Guid Id { get; set; }         // VehicleId.Value
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime ManufactureDate { get; set; }
        public bool IsRented { get; set; }
    }
}
