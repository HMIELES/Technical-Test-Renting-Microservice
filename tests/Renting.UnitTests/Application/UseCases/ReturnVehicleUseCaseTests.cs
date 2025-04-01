using System;
using System.Threading.Tasks;
using Moq;
using Renting.Application.DTOs;
using Renting.Application.Interfaces.OutputPorts;
using Renting.Application.UseCases.ReturnVehicle;
using Renting.Domain.Entities;
using Renting.Domain.Interfaces;
using Renting.Domain.ValueObjects;
using Xunit;

namespace Renting.UnitTests.Application.UseCases
{
    public class ReturnVehicleUseCaseTests
    {
        [Fact]
        public async Task Should_Return_Vehicle_Successfully()
        {
            // Arrange
            var rentalId = new RentalId(Guid.NewGuid());
            var rental = new Rental(
                rentalId,
                new CustomerId(Guid.NewGuid()),
                new VehicleId(Guid.NewGuid())
            );

            var rentalRepository = new Mock<IRentalRepository>();
            rentalRepository.Setup(r => r.GetByIdAsync(rentalId)).ReturnsAsync(rental);

            var unitOfWork = new Mock<IUnitOfWork>();
            var outputPort = new Mock<IReturnVehicleOutputPort>();

            var useCase = new ReturnVehicleUseCase(
                rentalRepository.Object,
                unitOfWork.Object,
                outputPort.Object
            );

            var request = new ReturnVehicleRequest
            {
                RentalId = rentalId.Value
            };

            // Act
            await useCase.Execute(request);

            // Assert
            unitOfWork.Verify(u => u.SaveAsync(), Times.Once);
            outputPort.Verify(o => o.Ok(It.IsAny<ReturnVehicleResponse>()), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Return_If_Rental_Does_Not_Exist()
        {
            // Arrange
            var rentalId = new RentalId(Guid.NewGuid());

            var rentalRepository = new Mock<IRentalRepository>();
            rentalRepository.Setup(r => r.GetByIdAsync(rentalId)).ReturnsAsync((Rental)null); // Simula que no existe

            var unitOfWork = new Mock<IUnitOfWork>();
            var outputPort = new Mock<IReturnVehicleOutputPort>();

            var useCase = new ReturnVehicleUseCase(
                rentalRepository.Object,
                unitOfWork.Object,
                outputPort.Object
            );

            var request = new ReturnVehicleRequest
            {
                RentalId = rentalId.Value
            };

            // Act
            await useCase.Execute(request);

            // Assert
            unitOfWork.Verify(u => u.SaveAsync(), Times.Never);
            outputPort.Verify(o => o.RentalNotFound("Rental not found."), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Return_If_Rental_Is_Already_Returned()
        {
            // Arrange
            var rentalId = new RentalId(Guid.NewGuid());

            var rental = new Rental(
                rentalId,
                new CustomerId(Guid.NewGuid()),
                new VehicleId(Guid.NewGuid())
            );

            rental.Return(); // Marcar el alquiler como ya devuelto

            var rentalRepository = new Mock<IRentalRepository>();
            rentalRepository.Setup(r => r.GetByIdAsync(rentalId)).ReturnsAsync(rental);

            var unitOfWork = new Mock<IUnitOfWork>();
            var outputPort = new Mock<IReturnVehicleOutputPort>();

            var useCase = new ReturnVehicleUseCase(
                rentalRepository.Object,
                unitOfWork.Object,
                outputPort.Object
            );

            var request = new ReturnVehicleRequest
            {
                RentalId = rentalId.Value
            };

            // Act
            await useCase.Execute(request);

            // Assert
            unitOfWork.Verify(u => u.SaveAsync(), Times.Never);
            outputPort.Verify(o => o.RentalAlreadyReturned("Rental has already been returned."), Times.Once);
        }
    }
}
