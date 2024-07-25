using Microsoft.EntityFrameworkCore;
using SignalRChatDemo.Models;

namespace SignalRChatDemo.Data
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }

        public DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatMessage>().ToTable("ChatMessages");
        }
    }
}
