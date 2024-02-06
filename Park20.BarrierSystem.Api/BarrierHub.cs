using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Park20.BarrierSystem.Api
{
    public class BarrierHub : Hub
    {
        public async Task OpenBarrier()
        {
            await Clients.All.SendAsync("OpenBarrier");
        }

        public async Task CloseBarrier()
        {
            await Clients.All.SendAsync("CloseBarrier");
        }
    }
}
