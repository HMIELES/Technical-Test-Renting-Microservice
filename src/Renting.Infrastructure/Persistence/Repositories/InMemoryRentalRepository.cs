using System.Collections.Concurrent;
using System.Threading.Tasks;
using Renting.Domain.Entities;
using Renting.Domain.Interfaces;
using Renting.Domain.ValueObjects;

namespace Renting.Infrastructure.Persistence.Repositories
{
    public class InMemoryRentalRepository : IRentalRepository
    {
        private static readonly ConcurrentDictionary<RentalId, Rental> _rentals = new();

        public Task<Rental> GetByIdAsync(RentalId id)
        {
            _rentals.TryGetValue(id, out var rental);
            return Task.FromResult(rental);
        }

        public Task AddAsync(Rental rental)
        {
            _rentals[rental.Id] = rental;
            return Task.CompletedTask;
        }
    }
}
