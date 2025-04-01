using Renting.Domain.Entities;
using Renting.Domain.ValueObjects;
using Renting.Domain.Exceptions;
using System;

namespace Renting.Domain.Entities
{
    public class Customer
    {
        public CustomerId Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public bool HasActiveRental { get; private set; }

        public Customer(CustomerId id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            HasActiveRental = false;
        }

        public void StartRental()
        {
            if (HasActiveRental)
                throw new CustomerAlreadyHasActiveRentalException();

            HasActiveRental = true;
        }

        public void EndRental()
        {
            if (!HasActiveRental)
                throw new InvalidOperationException("Customer has no active rental to end.");

            HasActiveRental = false;
        }
    }
}
