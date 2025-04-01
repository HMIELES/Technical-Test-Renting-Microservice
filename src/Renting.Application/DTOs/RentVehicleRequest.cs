using System;
using System.ComponentModel.DataAnnotations;

namespace Renting.Application.DTOs
{
    public class RentVehicleRequest
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid VehicleId { get; set; }
    }
}
