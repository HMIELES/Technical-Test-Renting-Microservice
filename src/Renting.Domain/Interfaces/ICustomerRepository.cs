using System.Threading.Tasks;
using Renting.Domain.Entities;
using Renting.Domain.ValueObjects;

namespace Renting.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(CustomerId id);
        Task AddAsync(Customer customer);
    }
}
