using Renting.Domain.Entities;
using Renting.Domain.ValueObjects;
using System;

namespace Renting.Domain.ValueObjects
{
    public readonly struct VehicleId
    {
        public Guid Value { get; }

        public VehicleId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Vehicle ID cannot be empty.", nameof(value));

            Value = value;
        }

        public override string ToString() => Value.ToString();
    }
}

