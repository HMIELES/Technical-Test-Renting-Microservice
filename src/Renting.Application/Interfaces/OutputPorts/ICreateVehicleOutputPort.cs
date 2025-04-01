using Renting.Application.DTOs;

namespace Renting.Application.Interfaces.OutputPorts
{
    public interface ICreateVehicleOutputPort
    {
        void Ok(CreateVehicleResponse response);
        void Invalid(string message);
        void VehicleTooOld(string message);
    }
}

