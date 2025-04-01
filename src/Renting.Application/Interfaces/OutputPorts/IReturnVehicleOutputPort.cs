using Renting.Application.DTOs;

namespace Renting.Application.Interfaces.OutputPorts
{
    public interface IReturnVehicleOutputPort
    {
        void Ok(ReturnVehicleResponse response);
        void Invalid(string message);
        void RentalNotFound(string message);
        void RentalAlreadyReturned(string message);
    }
}
