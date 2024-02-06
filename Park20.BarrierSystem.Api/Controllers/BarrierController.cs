using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Park20.BarrierSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarrierController : ControllerBase
    {
        private readonly IHubContext<BarrierHub> _hubContext;

        public BarrierController(IHubContext<BarrierHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet("OpenBarrier")]
        public async Task<IActionResult> OpenEntryBarrier()
        {

            await _hubContext.Clients.All.SendAsync("OpenBarrier");

            return Ok();
        }

        [HttpGet("CloseBarrier")]
        public async Task<IActionResult> CloseEntryBarrier()
        {

            await _hubContext.Clients.All.SendAsync("CloseBarrier");

            return Ok();
        }
    }
}
