using System;
using System.Threading.Tasks;
using Moq;
using Renting.Application.DTOs;
using Renting.Application.Interfaces.OutputPorts;
using Renting.Application.UseCases.CreateVehicle;
using Renting.Domain.Interfaces;
using Renting.Domain.Entities;
using Xunit;

namespace Renting.UnitTests.Application.UseCases
{
    public class CreateVehicleUseCaseTests
    {
        [Fact]
        public async Task Should_Create_Vehicle_Successfully()
        {
            // Arrange
            var vehicleRepository = new Mock<IVehicleRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var outputPort = new Mock<ICreateVehicleOutputPort>();

            var useCase = new CreateVehicleUseCase(vehicleRepository.Object, unitOfWork.Object, outputPort.Object);

            var request = new CreateVehicleRequest
            {
                Brand = "Toyota",
                Model = "Yaris",
                ManufactureDate = DateTime.UtcNow.AddYears(-2)
            };

            // Act
            await useCase.Execute(request);

            // Assert
            vehicleRepository.Verify(v => v.AddAsync(It.IsAny<Vehicle>()), Times.Once);
            unitOfWork.Verify(u => u.SaveAsync(), Times.Once);
            outputPort.Verify(o => o.Ok(It.IsAny<CreateVehicleResponse>()), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Create_Vehicle_If_Too_Old()
        {
            // Arrange
            var vehicleRepository = new Mock<IVehicleRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var outputPort = new Mock<ICreateVehicleOutputPort>();

            var useCase = new CreateVehicleUseCase(vehicleRepository.Object, unitOfWork.Object, outputPort.Object);

            var request = new CreateVehicleRequest
            {
                Brand = "Ford",
                Model = "Escort",
                ManufactureDate = DateTime.UtcNow.AddYears(-6) // Más de 5 años
            };

            // Act
            await useCase.Execute(request);

            // Assert
            vehicleRepository.Verify(v => v.AddAsync(It.IsAny<Vehicle>()), Times.Never);
            unitOfWork.Verify(u => u.SaveAsync(), Times.Never);
            outputPort.Verify(o => o.Invalid(It.IsAny<string>()), Times.Once);
        }

    }
}
