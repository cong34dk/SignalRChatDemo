using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRChatDemo.Hubs
{
    public class ChatHub : Hub
    {
        // Phương thức gửi tin nhắn đến tất cả client
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message, null);
        }

        // Phương thức gửi hình ảnh đến tất cả client
        public async Task SendImage(string user, string imageUrl)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, null, imageUrl);
        }

        // Phương thức thông báo rằng người dùng đang nhập
        public async Task Typing(string user)
        {
            await Clients.Others.SendAsync("UserTyping", user);
        }

        // Phương thức thông báo rằng người dùng đã ngừng nhập
        public async Task StopTyping(string user)
        {
            await Clients.Others.SendAsync("UserStoppedTyping", user);
        }
    }
}
