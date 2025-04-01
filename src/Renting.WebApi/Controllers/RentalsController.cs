using Microsoft.AspNetCore.Mvc;
using Renting.Application.DTOs;
using Renting.Application.Interfaces.InputPorts;
using Renting.WebApi.Presenters;
using System.Threading.Tasks;

namespace Renting.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly IRentVehicleUseCase _useCase;
        private readonly RentVehiclePresenter _presenter;

        public RentalsController(IRentVehicleUseCase useCase, RentVehiclePresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        [HttpPost]
        public async Task<IActionResult> RentVehicle([FromBody] RentVehicleRequest request)
        {
            await _useCase.Execute(request);
            return _presenter.Result;
        }
    }
}
