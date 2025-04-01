using Renting.Domain.Interfaces;

namespace Renting.Infrastructure.UnitOfWork
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        public Task SaveAsync()
        {
            // No persistence needed for in-memory simulation
            return Task.CompletedTask;
        }
    }
}
