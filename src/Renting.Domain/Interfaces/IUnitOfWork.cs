using System.Threading.Tasks;

namespace Renting.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
