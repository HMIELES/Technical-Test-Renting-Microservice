using Microsoft.AspNetCore.Mvc;
using Renting.Application.DTOs;
using Renting.Application.Interfaces.InputPorts;
using Renting.WebApi.Presenters;
using Renting.Application.UseCases.CreateVehicle;
using Renting.WebApi.ViewModels;
using System.Threading.Tasks;

namespace Renting.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly ICreateVehicleUseCase _useCase;
        private readonly CreateVehiclePresenter _presenter;

        public VehiclesController(ICreateVehicleUseCase useCase, CreateVehiclePresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleRequest request)
        {
            await _useCase.Execute(request);
            return _presenter.Result;
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("🚗 Renting microservice is alive!");
        }

    }
}
