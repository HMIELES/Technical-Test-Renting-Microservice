using System;
using System.ComponentModel.DataAnnotations;

namespace Renting.Application.DTOs
{
    public class ReturnVehicleRequest
    {
        [Required]
        public Guid RentalId { get; set; }
    }
}
