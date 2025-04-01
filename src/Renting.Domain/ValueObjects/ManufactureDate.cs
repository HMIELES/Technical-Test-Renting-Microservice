using Renting.Domain.Entities;
using Renting.Domain.ValueObjects;
using System;

namespace Renting.Domain.ValueObjects
{
    public readonly struct ManufactureDate
    {
        public DateTime Value { get; }

        public ManufactureDate(DateTime value)
        {
            if (value > DateTime.UtcNow)
                throw new ArgumentException("Manufacture date cannot be in the future.");

            Value = value;
        }

        public bool IsOlderThanYears(int years)
        {
            return Value < DateTime.UtcNow.AddYears(-years);
        }

        public override string ToString() => Value.ToString("yyyy-MM-dd");
    }
}
