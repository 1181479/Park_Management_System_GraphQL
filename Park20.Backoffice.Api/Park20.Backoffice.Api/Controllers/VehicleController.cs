using Microsoft.AspNetCore.Mvc;
using Park20.Backoffice.Application.Services;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.IServices;

namespace Park20.Backoffice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IParkLogService _parkLogService;
        private readonly IParkService _parkService;
        private readonly IPaymentService _paymentService;

        public VehicleController(IVehicleService vehicleService, IParkLogService parkLogService, IParkService parkService, IPaymentService paymentService)
        {
            _vehicleService = vehicleService;
            _parkLogService = parkLogService;
            _parkService = parkService;
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] CreateVehicleRequestDto createVehicleRequestDto)
        {
            var vehicle = await _vehicleService.AddVehicleToUser(createVehicleRequestDto);

            return Ok(vehicle);
        }

        [HttpGet("GetAllFromUser/{username}")]
        public async Task<IActionResult> GetVehicleListFromUser(string username)
        {
            var vehicleList = await _vehicleService.GetVehicleListFromUser(username);
            return Ok(vehicleList);
        }

        [HttpGet("GetVehicleByLicencePlate")]
        public async Task<IActionResult> GetVehicleByLicencePlate([FromQuery]string license)
        {
            if (license == null || license.Trim().Length == 0)
            {
                return BadRequest("Vehicle information cannot be null");
            }
            var vehicle = await _vehicleService.GetVehicle(license);
            return Ok(vehicle != null);
        }

        [HttpPost("ParkCar")]
        public async Task<IActionResult> ParkCar([FromBody] ParkingSpotsUpdateRequestDto requestDto)
        {
            await _parkLogService.StartingCountingTimeOperation(requestDto.LicensePlate, requestDto.ParkName);
            var result = await _parkLogService.UpdateAvailableParkingSpots(requestDto.ParkName, requestDto.LicensePlate, requestDto.IsEntrance);
            return Ok(result);
        }

        [HttpPost("LeavePark")]
        public async Task<IActionResult> LeavePark([FromBody] ParkingSpotsUpdateRequestDto requestDto)
        {
            await _parkLogService.StopCountingTimeOperation(requestDto.LicensePlate, requestDto.ParkName);

            var totalCost = await _paymentService.CalculateCost(requestDto.ParkName, requestDto.LicensePlate);

            var payment = await _paymentService.MakePayment(requestDto.LicensePlate, totalCost);

            await _parkLogService.UpdateAvailableParkingSpots(requestDto.ParkName, requestDto.LicensePlate, requestDto.IsEntrance);

            return Ok(payment);
        }

    }
}
