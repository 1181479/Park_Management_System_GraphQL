using Microsoft.AspNetCore.Mvc;
using Park20.Backoffice.Application.Services;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Park20.Backoffice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkyWalletController(IParkyWalletService parkyWalletService) : ControllerBase
    {

        private readonly IParkyWalletService _parkyWalletService = parkyWalletService;

        [HttpPost("BulkAssign")]
        public async Task<IActionResult> BulkParky([FromBody] List<string> customerUsernames)
        {
            var customer = await _parkyWalletService.BulkParky(customerUsernames);
            return Ok(customer);
        }

        [HttpPatch("Bulk")]
        public async Task<IActionResult> UpdateBulkValue([FromBody] int value)
        {
            return Ok(await _parkyWalletService.UpdateBulkValue(value));
        }
        [HttpPatch("NewCustomer")]
        public async Task<IActionResult> UpdateNewCustomerValue([FromBody] int value)
        {
            return Ok(await _parkyWalletService.UpdateNewCustomerValue(value));
        }
        [HttpPatch("Parking")]
        public async Task<IActionResult> UpdateParkingValue([FromBody] int value)
        {
            return Ok(await _parkyWalletService.UpdateParkingValue(value));
        }
        [HttpGet("ParkingPerHourValue")]
        public async Task<IActionResult> GetParkingPerHourValue()
        {
            return Ok(await _parkyWalletService.GetParkingValueAsync());
        }
        [HttpGet("RegistryValue")]
        public async Task<IActionResult> GetRegistryValue()
        {
            return Ok(await _parkyWalletService.GetRegestryValue());
        }

        [HttpGet("BulkValue")]
        public async Task<IActionResult> GetBulkValue()
        {
            return Ok(await _parkyWalletService.GetBulkValue());
        }
        [HttpGet("ParkyWalletUsername")]
        public async Task<IActionResult> GetParkyWalletByUsername([FromQuery] string username)
        {
            return Ok(await _parkyWalletService.GetParkyWalletByUsername(username));
        }
    }
}
