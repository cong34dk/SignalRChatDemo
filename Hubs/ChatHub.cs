using Microsoft.AspNetCore.SignalR;

namespace SignalRChatDemo.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message, null);
        }

        public async Task SendImage(string user, string imageUrl)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, null, imageUrl);
        }
    }
}
