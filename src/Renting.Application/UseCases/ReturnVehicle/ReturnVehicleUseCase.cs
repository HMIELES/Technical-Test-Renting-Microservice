using System;
using System.Threading.Tasks;
using Renting.Application.DTOs;
using Renting.Application.Interfaces.InputPorts;
using Renting.Application.Interfaces.OutputPorts;
using Renting.Domain.Entities;
using Renting.Domain.Interfaces;
using Renting.Domain.ValueObjects;

namespace Renting.Application.UseCases.ReturnVehicle
{
    public class ReturnVehicleUseCase : IReturnVehicleUseCase
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReturnVehicleOutputPort _outputPort;

        public ReturnVehicleUseCase(
            IRentalRepository rentalRepository,
            IUnitOfWork unitOfWork,
            IReturnVehicleOutputPort outputPort)
        {
            _rentalRepository = rentalRepository;
            _unitOfWork = unitOfWork;
            _outputPort = outputPort;
        }

        public async Task Execute(ReturnVehicleRequest request)
        {
            try
            {
                var rentalId = new RentalId(request.RentalId);

                var rental = await _rentalRepository.GetByIdAsync(rentalId);
                if (rental == null)
                {
                    _outputPort.RentalNotFound("Rental not found.");
                    return;
                }

                if (!rental.IsActive)
                {
                    _outputPort.RentalAlreadyReturned("Rental has already been returned.");
                    return;
                }

                rental.Return();

                await _unitOfWork.SaveAsync();

                var response = new ReturnVehicleResponse(
                    rental.Id.Value,
                    rental.ReturnedAt!.Value
                );

                _outputPort.Ok(response);
            }
            catch (ArgumentException ex)
            {
                _outputPort.Invalid(ex.Message);
            }
        }
    }
}
