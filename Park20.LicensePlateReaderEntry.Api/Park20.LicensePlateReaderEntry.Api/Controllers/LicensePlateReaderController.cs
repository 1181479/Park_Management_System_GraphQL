using Microsoft.AspNetCore.Mvc;
using Park20.LicensePlateReader.Api.Services;
using Park20.LicensePlateReader.Core.IServices;

namespace Park20.LicensePlateReader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicensePlateReaderController : ControllerBase
    {
        private readonly ILicensePlateReaderService licensePlateReaderService;

        public LicensePlateReaderController(ILicensePlateReaderService licensePlateReaderService)
        {
            this.licensePlateReaderService = licensePlateReaderService;
        }

        [HttpGet("ReadLicensePlateEntrance")]
        public async Task<IActionResult> ReadLicensePlateEntrance()
        {
            var result = await licensePlateReaderService.ReadLicensePlateEntrance();
            return Ok(result);
        }
        [HttpPut]
        public IActionResult Update([FromBody] string newLicensePlate)
        {

            licensePlateReaderService.UpdateLicensePlate(newLicensePlate);
            return Ok(true);
        }
    }

}
