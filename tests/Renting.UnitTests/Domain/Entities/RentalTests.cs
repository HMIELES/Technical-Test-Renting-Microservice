using System;
using Renting.Domain.Entities;
using Renting.Domain.Exceptions;
using Renting.Domain.ValueObjects;
using Xunit;

namespace Renting.UnitTests.Domain.Entities
{
    public class RentalTests
    {
        [Fact]
        public void Should_Create_Rental_Successfully()
        {
            // Arrange
            var rentalId = new RentalId(Guid.NewGuid());
            var customerId = new CustomerId(Guid.NewGuid());
            var vehicleId = new VehicleId(Guid.NewGuid());

            // Act
            var rental = new Rental(rentalId, customerId, vehicleId);

            // Assert
            Assert.Equal(rentalId, rental.Id);
            Assert.Equal(customerId, rental.CustomerId);
            Assert.Equal(vehicleId, rental.VehicleId);
            Assert.True(rental.IsActive);
            Assert.Null(rental.ReturnedAt);
        }

        [Fact]
        public void Should_Mark_Rental_As_Returned()
        {
            // Arrange
            var rental = new Rental(
                new RentalId(Guid.NewGuid()),
                new CustomerId(Guid.NewGuid()),
                new VehicleId(Guid.NewGuid())
            );

            // Act
            rental.Return();

            // Assert
            Assert.False(rental.IsActive);
            Assert.NotNull(rental.ReturnedAt);
        }

        [Fact]
        public void Should_Throw_If_Rental_Is_Already_Returned()
        {
            // Arrange
            var rental = new Rental(
                new RentalId(Guid.NewGuid()),
                new CustomerId(Guid.NewGuid()),
                new VehicleId(Guid.NewGuid())
            );

            rental.Return();

            // Act & Assert
            Assert.Throws<RentalAlreadyReturnedException>(() => rental.Return());
        }
    }
}

