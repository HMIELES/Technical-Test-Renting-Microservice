using System.Collections.Concurrent;
using Renting.Domain.Entities;
using Renting.Domain.Interfaces;
using Renting.Domain.ValueObjects;

namespace Renting.Infrastructure.Persistence.Repositories
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        private static readonly ConcurrentDictionary<CustomerId, Customer> _customers = new();

        public Task<Customer> GetByIdAsync(CustomerId id)
        {
            _customers.TryGetValue(id, out var customer);
            return Task.FromResult(customer);
        }

        public Task AddAsync(Customer customer)
        {
            _customers[customer.Id] = customer;
            return Task.CompletedTask;
        }
    }
}
