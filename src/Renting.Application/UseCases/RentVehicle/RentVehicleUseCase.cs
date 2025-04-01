using System;
using System.Threading.Tasks;
using Renting.Application.DTOs;
using Renting.Application.Interfaces.InputPorts;
using Renting.Application.Interfaces.OutputPorts;
using Renting.Domain.Entities;
using Renting.Domain.Interfaces;
using Renting.Domain.ValueObjects;

namespace Renting.Application.UseCases.RentVehicle
{
    public class RentVehicleUseCase : IRentVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRentVehicleOutputPort _outputPort;

        public RentVehicleUseCase(
            IVehicleRepository vehicleRepository,
            ICustomerRepository customerRepository,
            IRentalRepository rentalRepository,
            IUnitOfWork unitOfWork,
            IRentVehicleOutputPort outputPort)
        {
            _vehicleRepository = vehicleRepository;
            _customerRepository = customerRepository;
            _rentalRepository = rentalRepository;
            _unitOfWork = unitOfWork;
            _outputPort = outputPort;
        }

        public async Task Execute(RentVehicleRequest request)
        {
            try
            {
                var customerId = new CustomerId(request.CustomerId);
                var vehicleId = new VehicleId(request.VehicleId);

                var customer = await _customerRepository.GetByIdAsync(customerId);
                if (customer == null)
                    throw new ArgumentException("Customer not found");

                var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
                if (vehicle == null)
                    throw new ArgumentException("Vehicle not found");

                if (customer.HasActiveRental)
                {
                    _outputPort.CustomerHasActiveRental("Customer already has an active rental.");
                    return;
                }

                if (vehicle.IsRented)
                {
                    _outputPort.VehicleNotAvailable("Vehicle is currently rented.");
                    return;
                }

                customer.StartRental();
                vehicle.Rent();

                var rental = new Rental(
                    id: new RentalId(Guid.NewGuid()),
                    customerId: customer.Id,
                    vehicleId: vehicle.Id
                );

                await _rentalRepository.AddAsync(rental);
                await _unitOfWork.SaveAsync();

                var response = new RentVehicleResponse(
                    rental.Id.Value,
                    customer.Id.Value,
                    vehicle.Id.Value,
                    rental.RentedAt
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
