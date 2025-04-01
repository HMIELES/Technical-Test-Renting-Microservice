using Renting.Domain.Entities;
using Renting.Domain.ValueObjects;
using System;

namespace Renting.Domain.ValueObjects
{
    public readonly struct RentalId
    {
        public Guid Value { get; }

        public RentalId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Rental ID cannot be empty.", nameof(value));

            Value = value;
        }

        public override string ToString() => Value.ToString();
    }
}

