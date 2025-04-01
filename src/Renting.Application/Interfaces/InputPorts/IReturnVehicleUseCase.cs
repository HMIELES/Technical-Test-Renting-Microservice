using System.Threading.Tasks;
using Renting.Application.DTOs;

namespace Renting.Application.Interfaces.InputPorts
{
    public interface IReturnVehicleUseCase
    {
        Task Execute(ReturnVehicleRequest request);
    }
}
