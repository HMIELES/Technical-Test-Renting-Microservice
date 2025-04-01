using System;

namespace Renting.Domain.ValueObjects
{
    public readonly struct CustomerId
    {
        public Guid Value { get; }

        public CustomerId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Customer ID cannot be empty.", nameof(value));

            Value = value;
        }

        public override string ToString() => Value.ToString();
    }
}

