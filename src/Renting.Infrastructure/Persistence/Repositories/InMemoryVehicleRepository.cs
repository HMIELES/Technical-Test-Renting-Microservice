using System.Collections.Concurrent;
using Renting.Domain.Entities;
using Renting.Domain.Interfaces;
using Renting.Domain.ValueObjects;


namespace Renting.Infrastructure.Persistence.Repositories
{
    public class InMemoryVehicleRepository : IVehicleRepository
    {
        private static readonly ConcurrentDictionary<VehicleId, Vehicle> _vehicles = new();

        public Task AddAsync(Vehicle vehicle)
        {
            _vehicles[vehicle.Id] = vehicle;
            return Task.CompletedTask;
        }

        public Task<Vehicle> GetByIdAsync(VehicleId id)
        {
            _vehicles.TryGetValue(id, out var vehicle);
            return Task.FromResult(vehicle);
        }

        public Task<bool> ExistsAsync(VehicleId id)
        {
            return Task.FromResult(_vehicles.ContainsKey(id));
        }
    }
}
