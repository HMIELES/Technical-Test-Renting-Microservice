using System;
using System.ComponentModel.DataAnnotations;

namespace Renting.Application.DTOs
{
    public class CreateVehicleRequest
    {
        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public DateTime ManufactureDate { get; set; }
    }
}
