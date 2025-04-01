using System;
using System.Threading.Tasks;
using Moq;
using Renting.Application.DTOs;
using Renting.Application.Interfaces.OutputPorts;
using Renting.Application.UseCases.RentVehicle;
using Renting.Domain.Entities;
using Renting.Domain.Interfaces;
using Renting.Domain.ValueObjects;
using Xunit;

namespace Renting.UnitTests.Application.UseCases
{
    public class RentVehicleUseCaseTests
    {
        [Fact]
        public async Task Should_Rent_Vehicle_Successfully()
        {
            // Arrange
            var customerId = new CustomerId(Guid.NewGuid());
            var vehicleId = new VehicleId(Guid.NewGuid());

            var customer = new Customer(customerId, "Himer", "Mieles");
            var vehicle = new Vehicle(vehicleId, "Toyota", "Corolla", new ManufactureDate(DateTime.UtcNow.AddYears(-2)));

            var rentalRepository = new Mock<IRentalRepository>();
            var customerRepository = new Mock<ICustomerRepository>();
            var vehicleRepository = new Mock<IVehicleRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var outputPort = new Mock<IRentVehicleOutputPort>();

            customerRepository.Setup(r => r.GetByIdAsync(customerId)).ReturnsAsync(customer);
            vehicleRepository.Setup(r => r.GetByIdAsync(vehicleId)).ReturnsAsync(vehicle);

            var useCase = new RentVehicleUseCase(
                vehicleRepository.Object,
                customerRepository.Object,
                rentalRepository.Object,
                unitOfWork.Object,
                outputPort.Object
            );

            var request = new RentVehicleRequest
            {
                CustomerId = customerId.Value,
                VehicleId = vehicleId.Value
            };

            // Act
            await useCase.Execute(request);

            // Assert
            rentalRepository.Verify(r => r.AddAsync(It.IsAny<Rental>()), Times.Once);
            unitOfWork.Verify(u => u.SaveAsync(), Times.Once);
            outputPort.Verify(o => o.Ok(It.IsAny<RentVehicleResponse>()), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Rent_If_Customer_Has_Active_Rental()
        {
            // Arrange
            var customerId = new CustomerId(Guid.NewGuid());
            var vehicleId = new VehicleId(Guid.NewGuid());

            var customer = new Customer(customerId, "Laura", "Ramírez");
            customer.StartRental(); // Cliente ya tiene un alquiler activo

            var vehicle = new Vehicle(vehicleId, "Kia", "Rio", new ManufactureDate(DateTime.UtcNow.AddYears(-2)));

            var rentalRepository = new Mock<IRentalRepository>();
            var customerRepository = new Mock<ICustomerRepository>();
            var vehicleRepository = new Mock<IVehicleRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var outputPort = new Mock<IRentVehicleOutputPort>();

            customerRepository.Setup(r => r.GetByIdAsync(customerId)).ReturnsAsync(customer);
            vehicleRepository.Setup(r => r.GetByIdAsync(vehicleId)).ReturnsAsync(vehicle);

            var useCase = new RentVehicleUseCase(
                vehicleRepository.Object,
                customerRepository.Object,
                rentalRepository.Object,
                unitOfWork.Object,
                outputPort.Object
            );

            var request = new RentVehicleRequest
            {
                CustomerId = customerId.Value,
                VehicleId = vehicleId.Value
            };

            // Act
            await useCase.Execute(request);

            // Assert
            rentalRepository.Verify(r => r.AddAsync(It.IsAny<Rental>()), Times.Never);
            unitOfWork.Verify(u => u.SaveAsync(), Times.Never);
            outputPort.Verify(o => o.CustomerHasActiveRental(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Rent_If_Vehicle_Is_Already_Rented()
        {
            // Arrange
            var customerId = new CustomerId(Guid.NewGuid());
            var vehicleId = new VehicleId(Guid.NewGuid());

            var customer = new Customer(customerId, "Pedro", "García");

            var vehicle = new Vehicle(vehicleId, "Chevrolet", "Onix", new ManufactureDate(DateTime.UtcNow.AddYears(-1)));
            vehicle.Rent(); // Marcamos el vehículo como ya rentado

            var rentalRepository = new Mock<IRentalRepository>();
            var customerRepository = new Mock<ICustomerRepository>();
            var vehicleRepository = new Mock<IVehicleRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var outputPort = new Mock<IRentVehicleOutputPort>();

            customerRepository.Setup(r => r.GetByIdAsync(customerId)).ReturnsAsync(customer);
            vehicleRepository.Setup(r => r.GetByIdAsync(vehicleId)).ReturnsAsync(vehicle);

            var useCase = new RentVehicleUseCase(
                vehicleRepository.Object,
                customerRepository.Object,
                rentalRepository.Object,
                unitOfWork.Object,
                outputPort.Object
            );

            var request = new RentVehicleRequest
            {
                CustomerId = customerId.Value,
                VehicleId = vehicleId.Value
            };

            // Act
            await useCase.Execute(request);

            // Assert
            rentalRepository.Verify(r => r.AddAsync(It.IsAny<Rental>()), Times.Never);
            unitOfWork.Verify(u => u.SaveAsync(), Times.Never);
            outputPort.Verify(o => o.VehicleNotAvailable(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Rent_If_Customer_Does_Not_Exist()
        {
            // Arrange
            var customerId = new CustomerId(Guid.NewGuid());
            var vehicleId = new VehicleId(Guid.NewGuid());

            // Cliente no encontrado → retornamos null
            var customerRepository = new Mock<ICustomerRepository>();
            customerRepository.Setup(r => r.GetByIdAsync(customerId)).ReturnsAsync((Customer)null);

            var vehicle = new Vehicle(vehicleId, "Mazda", "3", new ManufactureDate(DateTime.UtcNow.AddYears(-2)));
            var vehicleRepository = new Mock<IVehicleRepository>();
            vehicleRepository.Setup(r => r.GetByIdAsync(vehicleId)).ReturnsAsync(vehicle);

            var rentalRepository = new Mock<IRentalRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var outputPort = new Mock<IRentVehicleOutputPort>();

            var useCase = new RentVehicleUseCase(
                vehicleRepository.Object,
                customerRepository.Object,
                rentalRepository.Object,
                unitOfWork.Object,
                outputPort.Object
            );

            var request = new RentVehicleRequest
            {
                CustomerId = customerId.Value,
                VehicleId = vehicleId.Value
            };

            // Act
            await useCase.Execute(request);

            // Assert
            rentalRepository.Verify(r => r.AddAsync(It.IsAny<Rental>()), Times.Never);
            unitOfWork.Verify(u => u.SaveAsync(), Times.Never);
            outputPort.Verify(o => o.Invalid("Customer not found"), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Rent_If_Vehicle_Does_Not_Exist()
        {
            // Arrange
            var customerId = new CustomerId(Guid.NewGuid());
            var vehicleId = new VehicleId(Guid.NewGuid());

            var customer = new Customer(customerId, "Carlos", "Martínez");

            var customerRepository = new Mock<ICustomerRepository>();
            customerRepository.Setup(r => r.GetByIdAsync(customerId)).ReturnsAsync(customer);

            // Vehículo no encontrado → retornamos null
            var vehicleRepository = new Mock<IVehicleRepository>();
            vehicleRepository.Setup(r => r.GetByIdAsync(vehicleId)).ReturnsAsync((Vehicle)null);

            var rentalRepository = new Mock<IRentalRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var outputPort = new Mock<IRentVehicleOutputPort>();

            var useCase = new RentVehicleUseCase(
                vehicleRepository.Object,
                customerRepository.Object,
                rentalRepository.Object,
                unitOfWork.Object,
                outputPort.Object
            );

            var request = new RentVehicleRequest
            {
                CustomerId = customerId.Value,
                VehicleId = vehicleId.Value
            };

            // Act
            await useCase.Execute(request);

            // Assert
            rentalRepository.Verify(r => r.AddAsync(It.IsAny<Rental>()), Times.Never);
            unitOfWork.Verify(u => u.SaveAsync(), Times.Never);
            outputPort.Verify(o => o.Invalid("Vehicle not found"), Times.Once);
        }

    }
}
