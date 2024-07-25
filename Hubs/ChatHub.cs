using Microsoft.AspNetCore.SignalR;
using SignalRChatDemo.Data;
using SignalRChatDemo.Models;
using System.Threading.Tasks;

namespace SignalRChatDemo.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatDbContext _context;

        public ChatHub(ChatDbContext context)
        {
            _context = context;
        }

        //Người dùng nhập tin nhắn hoặc chọn ảnh và nhấn nút "Send".
        //Server nhận được yêu cầu từ client và thực hiện 2 action song song
        //1. Lưu tin nhắn hoặc đường dẫn ảnh vào cơ sở dữ liệu
        //2. Gửi thông điệp tới tất cả các client đã kết nối để cập nhật giao diện thời gian thực.
        public async Task SendMessage(string user, string message)
        {
            try
            {
                // Lưu tin nhắn vào cơ sở dữ liệu
                var chatMessage = new ChatMessage
                {
                    User = user,
                    Message = message,
                    ImageUrl = null,
                    Timestamp = DateTime.UtcNow
                };

                _context.ChatMessages.Add(chatMessage);
                await _context.SaveChangesAsync(); // Lưu vào database

                // Gửi tin nhắn đến tất cả client
                await Clients.All.SendAsync("ReceiveMessage", user, message, null);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi và ném lại lỗi để client biết
                Console.WriteLine($"Error in SendMessage: {ex.Message}");
                throw;
            }
        }

        public async Task SendImage(string user, string imageUrl)
        {
            try
            {
                // Lưu tin nhắn có hình ảnh vào cơ sở dữ liệu
                var chatMessage = new ChatMessage
                {
                    User = user,
                    Message = null,
                    ImageUrl = imageUrl,
                    Timestamp = DateTime.UtcNow
                };

                _context.ChatMessages.Add(chatMessage);
                await _context.SaveChangesAsync(); // Lưu vào database

                // Gửi hình ảnh đến tất cả client
                await Clients.All.SendAsync("ReceiveMessage", user, null, imageUrl);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi và ném lại lỗi để client biết
                Console.WriteLine($"Error in SendImage: {ex.Message}");
                throw;
            }
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
