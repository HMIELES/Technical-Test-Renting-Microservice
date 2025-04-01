using Renting.Application.DTOs;
using Renting.Application.Interfaces.InputPorts;
using Renting.Application.Interfaces.OutputPorts;
using Renting.Domain.Entities;
using Renting.Domain.ValueObjects;
using Renting.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Renting.Application.UseCases.CreateVehicle
{
    public class CreateVehicleUseCase : ICreateVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICreateVehicleOutputPort _outputPort;

        public CreateVehicleUseCase(
            IVehicleRepository vehicleRepository,
            IUnitOfWork unitOfWork,
            ICreateVehicleOutputPort outputPort)
        {
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
            _outputPort = outputPort;
        }

        public async Task Execute(CreateVehicleRequest request)
        {
            try
            {
                var id = new VehicleId(Guid.NewGuid());
                var manufactureDate = new ManufactureDate(request.ManufactureDate);

                var vehicle = new Vehicle(
                    id,
                    request.Brand,
                    request.Model,
                    manufactureDate
                );

                await _vehicleRepository.AddAsync(vehicle);
                await _unitOfWork.SaveAsync();

                var response = new CreateVehicleResponse(
                    vehicle.Id.Value,
                    vehicle.Brand,
                    vehicle.Model,
                    vehicle.ManufactureDate.Value
                );

                _outputPort.Ok(response);
            }
            catch (ArgumentException ex)
            {
                _outputPort.Invalid(ex.Message);
            }
            catch (Domain.Exceptions.VehicleTooOldException ex)
            {
                _outputPort.Invalid(ex.Message);
            }
        }
    }
}

