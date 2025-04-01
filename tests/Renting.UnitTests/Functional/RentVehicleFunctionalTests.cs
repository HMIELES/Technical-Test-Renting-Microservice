using System;
using System.Threading.Tasks;
using Renting.Application.DTOs;
using Renting.Application.Interfaces.OutputPorts;
using Renting.Application.UseCases.RentVehicle;
using Renting.Domain.Entities;
using Renting.Domain.ValueObjects;
using Renting.Infrastructure.Persistence.Repositories;
using Renting.Infrastructure.UnitOfWork;
using Xunit;

namespace Renting.UnitTests.Functional
{
    public class RentVehicleFunctionalTests
    {
        [Fact]
        public async Task Should_Rent_Vehicle_With_InMemory_Repositories()
        {
            // Arrange
            var customerId = new CustomerId(Guid.NewGuid());
            var vehicleId = new VehicleId(Guid.NewGuid());

            var customer = new Customer(customerId, "Himer", "Mieles");
            var vehicle = new Vehicle(vehicleId, "Chevrolet", "Tracker", new ManufactureDate(DateTime.UtcNow.AddYears(-1)));

            var customerRepo = new InMemoryCustomerRepository();
            var vehicleRepo = new InMemoryVehicleRepository();
            var rentalRepo = new InMemoryRentalRepository();
            var unitOfWork = new FakeUnitOfWork();
            var outputPort = new RentVehicleOutputPortSpy();

            await customerRepo.AddAsync(customer);
            await vehicleRepo.AddAsync(vehicle);

            var useCase = new RentVehicleUseCase(
                vehicleRepo,
                customerRepo,
                rentalRepo,
                unitOfWork,
                outputPort
            );

            var request = new RentVehicleRequest
            {
                CustomerId = customerId.Value,
                VehicleId = vehicleId.Value
            };

            // Act
            await useCase.Execute(request);

            // Assert
            Assert.True(outputPort.OkWasCalled);
        }
    }

    // Espía simple para capturar salida del OutputPort
    public class RentVehicleOutputPortSpy : IRentVehicleOutputPort
    {
        public bool OkWasCalled { get; private set; }

        public void Ok(RentVehicleResponse response)
        {
            OkWasCalled = true;
        }

        public void Invalid(string message) { }
        public void CustomerHasActiveRental(string message) { }
        public void VehicleNotAvailable(string message) { }
    }
}
