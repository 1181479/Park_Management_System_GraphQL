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


        [HttpGet("ReadLicensePlateExit")]
        public async Task<IActionResult> ReadLicensePlateExit()
        {
            var result = await licensePlateReaderService.ReadLicensePlateExit();
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
