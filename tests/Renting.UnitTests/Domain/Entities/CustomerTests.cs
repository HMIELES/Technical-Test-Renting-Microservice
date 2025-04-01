using System;
using Renting.Domain.Entities;
using Renting.Domain.Exceptions;
using Renting.Domain.ValueObjects;
using Xunit;

namespace Renting.UnitTests.Domain.Entities
{
    public class CustomerTests
    {
        [Fact]
        public void Should_Create_Customer_Successfully()
        {
            // Arrange
            var id = new CustomerId(Guid.NewGuid());

            // Act
            var customer = new Customer(id, "Juan", "Pérez");

            // Assert
            Assert.Equal("Juan", customer.FirstName);
            Assert.False(customer.HasActiveRental);
        }

        [Fact]
        public void Should_Throw_Exception_If_Customer_Has_Active_Rental()
        {
            // Arrange
            var id = new CustomerId(Guid.NewGuid());
            var customer = new Customer(id, "Ana", "López");

            customer.StartRental();

            // Act & Assert
            Assert.Throws<CustomerAlreadyHasActiveRentalException>(() =>
                customer.StartRental());
        }

        [Fact]
        public void Should_Allow_Customer_To_Return_Rental()
        {
            // Arrange
            var id = new CustomerId(Guid.NewGuid());
            var customer = new Customer(id, "Luis", "Gómez");

            customer.StartRental();

            // Act
            customer.EndRental();

            // Assert
            Assert.False(customer.HasActiveRental);
        }
    }
}
