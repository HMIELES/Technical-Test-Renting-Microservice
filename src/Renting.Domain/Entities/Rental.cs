using Renting.Domain.Exceptions;
using Renting.Domain.ValueObjects;
using System;

namespace Renting.Domain.Entities
{
    public class Rental
    {
        public RentalId Id { get; private set; }
        public CustomerId CustomerId { get; private set; }
        public VehicleId VehicleId { get; private set; }
        public DateTime RentedAt { get; private set; }
        public DateTime? ReturnedAt { get; private set; }

        public bool IsActive => ReturnedAt == null;

        public Rental(RentalId id, CustomerId customerId, VehicleId vehicleId)
        {
            if (id.Value == Guid.Empty)
                throw new ArgumentException("Rental ID is required.");

            Id = id;
            CustomerId = customerId;
            VehicleId = vehicleId;
            RentedAt = DateTime.UtcNow;
        }

        public void Return()
        {
            if (!IsActive)
                throw new RentalAlreadyReturnedException();

            ReturnedAt = DateTime.UtcNow;
        }
    }
}
