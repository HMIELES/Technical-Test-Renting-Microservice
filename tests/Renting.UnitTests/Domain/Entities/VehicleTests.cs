using System;
using Renting.Domain.Entities;
using Renting.Domain.ValueObjects;
using Renting.Domain.Exceptions;
using Xunit;

namespace Renting.UnitTests.Domain.Entities
{
    public class VehicleTests
    {
        [Fact]
        public void Should_Create_Vehicle_Successfully()
        {
            var id = new VehicleId(Guid.NewGuid());
            var manufactureDate = new ManufactureDate(DateTime.UtcNow.AddYears(-2));
            var vehicle = new Vehicle(id, "Toyota", "Corolla", manufactureDate);

            Assert.Equal("Toyota", vehicle.Brand);
            Assert.False(vehicle.IsRented);
        }

        [Fact]
        public void Should_Throw_If_Vehicle_Is_Too_Old()
        {
            var id = new VehicleId(Guid.NewGuid());
            var oldDate = new ManufactureDate(DateTime.UtcNow.AddYears(-6));

            Assert.Throws<VehicleTooOldException>(() =>
                new Vehicle(id, "OldCar", "ModelX", oldDate));
        }
    }
}
