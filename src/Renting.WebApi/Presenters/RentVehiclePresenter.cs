using Microsoft.AspNetCore.Mvc;
using Renting.Application.DTOs;
using Renting.Application.Interfaces.OutputPorts;

namespace Renting.WebApi.Presenters
{
    public class RentVehiclePresenter : IRentVehicleOutputPort
    {
        public IActionResult Result { get; private set; }

        public void Ok(RentVehicleResponse response)
        {
            Result = new OkObjectResult(response);
        }

        public void Invalid(string message)
        {
            Result = new BadRequestObjectResult(new { Error = message });
        }

        public void CustomerHasActiveRental(string message)
        {
            Result = new ConflictObjectResult(new { Error = message });
        }

        public void VehicleNotAvailable(string message)
        {
            Result = new UnprocessableEntityObjectResult(new { Error = message });
        }
    }
}
