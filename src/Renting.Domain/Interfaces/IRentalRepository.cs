using System.Threading.Tasks;
using Renting.Domain.Entities;
using Renting.Domain.ValueObjects;

namespace Renting.Domain.Interfaces
{
    public interface IRentalRepository
    {
        Task<Rental> GetByIdAsync(RentalId id);
        Task AddAsync(Rental rental);
    }
}
