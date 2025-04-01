using System;

namespace Renting.Application.DTOs
{
    public class ReturnVehicleResponse
    {
        public Guid RentalId { get; set; }
        public DateTime ReturnedAt { get; set; }

        public ReturnVehicleResponse(Guid rentalId, DateTime returnedAt)
        {
            RentalId = rentalId;
            ReturnedAt = returnedAt;
        }
    }
}
