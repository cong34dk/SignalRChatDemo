namespace SignalRChatDemo.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string? Message { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime Timestamp { get; set; }
    }

}
