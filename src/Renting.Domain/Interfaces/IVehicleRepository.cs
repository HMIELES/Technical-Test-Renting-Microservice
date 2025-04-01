using System.Threading.Tasks;
using Renting.Domain.Entities;
using Renting.Domain.ValueObjects;

namespace Renting.Domain.Interfaces
{
    public interface IVehicleRepository
    {
        Task AddAsync(Vehicle vehicle);
        Task<Vehicle> GetByIdAsync(VehicleId id);
        Task<bool> ExistsAsync(VehicleId id);
    }
}
