using System;
using System.ComponentModel.DataAnnotations;

namespace Renting.Application.DTOs
{
    public class CreateVehicleRequest
    {
        // Vehicle brand (e.g., Toyota, Ford) //
        [Required]
        public string Brand { get; set; }

        // Vehicle model (e.g., Corolla, Mustang) //
        [Required]
        public string Model { get; set; }

        // Manufacture date of the vehicle (must not be older than 5 years) //
        [Required]
        public DateTime ManufactureDate { get; set; }
    }
}
