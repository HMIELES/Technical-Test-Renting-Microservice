using Microsoft.AspNetCore.Mvc;
using Renting.Application.DTOs;
using Renting.Application.Interfaces.InputPorts;
using Renting.WebApi.Presenters;
using System.Threading.Tasks;

namespace Renting.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReturnsController : ControllerBase
    {
        private readonly IReturnVehicleUseCase _useCase;
        private readonly ReturnVehiclePresenter _presenter;

        public ReturnsController(IReturnVehicleUseCase useCase, ReturnVehiclePresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        [HttpPost]
        public async Task<IActionResult> ReturnVehicle([FromBody] ReturnVehicleRequest request)
        {
            await _useCase.Execute(request);
            return _presenter.Result;
        }
    }
}

