using Microsoft.AspNetCore.SignalR;

namespace Park20.BarrierSystem.Api
{
    public class DisplayHub : Hub
    {
        public async Task ShowGeneralMessage()
        {
            await Clients.All.SendAsync("GeneralMessage");
        }

        public async Task ShowWelcomeMessage()
        {
            await Clients.All.SendAsync("WelcomeMessage");
        }

        public async Task ShowGoodbyeMessage(decimal parkyCoinsSpent, decimal otherPaymentMethodSpent)
        {
            await Clients.All.SendAsync("GoodbyeMessage", parkyCoinsSpent, otherPaymentMethodSpent);
        }
    }
}
