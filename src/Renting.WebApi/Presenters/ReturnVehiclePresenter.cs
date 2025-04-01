using Microsoft.AspNetCore.Mvc;
using Renting.Application.DTOs;
using Renting.Application.Interfaces.OutputPorts;

namespace Renting.WebApi.Presenters
{
    public class ReturnVehiclePresenter : IReturnVehicleOutputPort
    {
        public IActionResult Result { get; private set; }

        public void Ok(ReturnVehicleResponse response)
        {
            Result = new OkObjectResult(response);
        }

        public void Invalid(string message)
        {
            Result = new BadRequestObjectResult(new { Error = message });
        }

        public void RentalNotFound(string message)
        {
            Result = new NotFoundObjectResult(new { Error = message });
        }

        public void RentalAlreadyReturned(string message)
        {
            Result = new ConflictObjectResult(new { Error = message });
        }
    }
}
