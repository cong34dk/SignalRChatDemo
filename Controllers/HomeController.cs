using Microsoft.AspNetCore.Mvc;
using SignalRChatDemo.Data;
using SignalRChatDemo.Models;
using System.Diagnostics;

namespace SignalRChatDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ChatDbContext _context;

        public HomeController(ChatDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var chatHistory = _context.ChatMessages
                .OrderByDescending(m => m.Timestamp)
                .Take(50)
                .ToList();

            return View(chatHistory);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
