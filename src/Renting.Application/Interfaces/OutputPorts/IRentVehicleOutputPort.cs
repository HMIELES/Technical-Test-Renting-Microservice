using Renting.Application.DTOs;

namespace Renting.Application.Interfaces.OutputPorts
{
    public interface IRentVehicleOutputPort
    {
        void Ok(RentVehicleResponse response);
        void Invalid(string message);
        void CustomerHasActiveRental(string message);
        void VehicleNotAvailable(string message);
    }
}
