using Microsoft.AspNetCore.Mvc;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Park20.Backoffice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkController : ControllerBase
    {

        private readonly IParkService _parkService;
        private readonly IVehicleService _vehicleService;

        public ParkController(IParkService parkService, IVehicleService vehicleService, IParkLogService parkLogService)
        {
            _parkService = parkService;
            _vehicleService = vehicleService;
        }

        [HttpGet("GetAvailableSpace")]
        public async Task<IActionResult> GetAvailableSpace([FromQuery] ParkCheckRequestDto parkCheckRequestDto)
        {
            if (parkCheckRequestDto == null)
            {
                return BadRequest("Information cannot be null");
            }
            var vehicle = await _vehicleService.GetVehicle(parkCheckRequestDto.LicencePlate);
            if (vehicle == null)
                return BadRequest("Vehicle not found");
            var availableSpace = await _parkService.GetAvailableSpace(vehicle.Type, parkCheckRequestDto.ParkName);

            if (availableSpace == null || !availableSpace.HasValue)
            {
                return BadRequest("Not found");
            }
            return Ok(availableSpace.Value);
        }

        [HttpGet("GetVehicleTypeAvailable")]
        public async Task<IActionResult> GetVehicleTypeAvailable([FromQuery] ParkCheckRequestDto parkCheckRequestDto)
        {
            if (parkCheckRequestDto == null)
            {
                return BadRequest("Information cannot be null");
            }
            var vehicle = await _vehicleService.GetVehicle(parkCheckRequestDto.LicencePlate);
            if (vehicle == null)
                return BadRequest("Vehicle not found");

            var vehicleTypeAvailable = await _parkService.GetVehicleTypeAvailable(vehicle.Type, parkCheckRequestDto.ParkName);

            if (vehicleTypeAvailable == null || !vehicleTypeAvailable.HasValue)
            {
                return BadRequest("Not found");
            }
            return Ok(vehicleTypeAvailable.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParks()
        {
            var parks = await _parkService.GetAllParks();
            return Ok(parks);
        }

        [HttpGet("GetParkingSpotsCount")]
        public async Task<IActionResult> GetParkingSpotsCount([FromQuery] string parkName)
        {
            var parks = await _parkService.GetNumberParkingSpots(parkName);
            return Ok(parks);
        }

        [HttpGet("GetAllParksByDistance")]
        public async Task<IActionResult> GetAllParksByDistance([FromQuery] double latitude, double longitude)
        {
            var parks = await _parkService.GetAllParksWithDistance(latitude, longitude);
            return Ok(parks);
        }
        
        [HttpPost("UpdatePriceTable")]
        public async Task<IActionResult> UpdatePriceTable([FromBody] PriceTableDto updatePriceTable)
        {
            var parks = await _parkService.UpdatePriceTable(updatePriceTable);
            return Ok(parks);
        }

        [HttpGet("GetParkNames")]
        public async Task<IActionResult> GetParkNames()
        {
            var parkNames = await _parkService.GetParkNames();
            return Ok(parkNames);
        }

        [HttpGet("GetParkingSpots")]
        public async Task<IActionResult> GetParkingSpots([FromQuery] string parkName)
        {
            var parkingSpots = await _parkService.GetParkingSpots(parkName);
            return Ok(parkingSpots);
        }

        [HttpPut("UpdateParkingSpotStatus")]
        public async Task<IActionResult> UpdateParkingSpotStatus([FromBody] UpdateParkingSpotStatusRequestDto updateParkingSpotStatusRequestDto)
        {
            var result =await _parkService.UpdateParkingSpotStatus(updateParkingSpotStatusRequestDto.Status, updateParkingSpotStatusRequestDto.ParkingSpotId);
            return Ok(result);
        }

        [HttpGet("GetPriceTableByParkName")]
        public async Task<IActionResult> GetPriceTableByParkName([FromQuery] string parkName)
        {
            var priceTable = await _parkService.GetPriceTableByParkName(parkName);
            return Ok(priceTable);
        }
    }
}
