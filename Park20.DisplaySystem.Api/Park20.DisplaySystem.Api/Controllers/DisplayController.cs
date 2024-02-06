using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Park20.BarrierSystem.Api;

namespace Park20.DisplaySystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisplayController : ControllerBase
    {
        private readonly IHubContext<DisplayHub> _hubContext;

        public DisplayController(IHubContext<DisplayHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet("ShowGeneralMessage")]
        public async Task<IActionResult> ShowGeneralMessage()
        {

            await _hubContext.Clients.All.SendAsync("GeneralMessage");

            return Ok();
        }

        [HttpGet("ShowWelcomeMessage")]
        public async Task<IActionResult> ShowWelcomeMessage()
        {

            await _hubContext.Clients.All.SendAsync("WelcomeMessage");

            return Ok();
        }

        [HttpGet("ShowGoodbyeMessage")]
        public async Task<IActionResult> ShowGoodbyeMessage([FromQuery] decimal parkyCoinsSpent, [FromQuery] decimal otherPaymentMethodSpent, [FromQuery] decimal totalCost)
        {

            await _hubContext.Clients.All.SendAsync("GoodbyeMessage", parkyCoinsSpent, otherPaymentMethodSpent, totalCost);

            return Ok();
        }

    }
}
