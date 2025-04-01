using Microsoft.AspNetCore.Mvc;
using Renting.Application.DTOs;
using Renting.Application.Interfaces.OutputPorts;

namespace Renting.WebApi.Presenters
{
    public class CreateVehiclePresenter : ICreateVehicleOutputPort
    {
        public IActionResult Result { get; private set; }

        public void Ok(CreateVehicleResponse response)
        {
            Result = new CreatedAtRouteResult(
                routeName: "GetVehicleById",  // Esta ruta se definirá después
                routeValues: new { id = response.VehicleId },
                value: response
            );
        }

        public void Invalid(string message)
        {
            Result = new BadRequestObjectResult(new { Error = message });
        }

        public void VehicleTooOld(string message)
        {
            Result = new UnprocessableEntityObjectResult(new { Error = message });
        }
    }
}