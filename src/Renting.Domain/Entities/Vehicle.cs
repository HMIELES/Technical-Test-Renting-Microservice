using System;
using Renting.Domain.ValueObjects;
using Renting.Domain.Exceptions;

namespace Renting.Domain.Entities
{
    public class Vehicle
    {
        public VehicleId Id { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public ManufactureDate ManufactureDate { get; private set; }
        public bool IsRented { get; private set; }

        public Vehicle(VehicleId id, string brand, string model, ManufactureDate manufactureDate)
        {
            if (string.IsNullOrWhiteSpace(brand))
                throw new ArgumentException("Brand is required.");

            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Model is required.");

            if (manufactureDate.IsOlderThanYears(5))
                throw new VehicleTooOldException();

            Id = id;
            Brand = brand;
            Model = model;
            ManufactureDate = manufactureDate;
            IsRented = false;
        }

        public void Rent()
        {
            if (IsRented)
                throw new VehicleAlreadyRentedException();

            IsRented = true;
        }

        public void Return()
        {
            if (!IsRented)
                throw new VehicleNotRentedException();

            IsRented = false;
        }
    }
}
