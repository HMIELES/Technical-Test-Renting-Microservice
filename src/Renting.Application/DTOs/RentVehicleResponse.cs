using System;

namespace Renting.Application.DTOs
{
    public class RentVehicleResponse
    {
        public Guid RentalId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid VehicleId { get; set; }
        public DateTime RentedAt { get; set; }

        public RentVehicleResponse(Guid rentalId, Guid customerId, Guid vehicleId, DateTime rentedAt)
        {
            RentalId = rentalId;
            CustomerId = customerId;
            VehicleId = vehicleId;
            RentedAt = rentedAt;
        }
    }
}
