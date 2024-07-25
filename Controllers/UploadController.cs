using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.IO;

namespace SignalRChatDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        //Image path folder
        private readonly string _uploadFolder;

        // Constructor để khởi tạo đường dẫn và tạo thư mục nếu chưa tồn tại
        public UploadController()
        {
            // Khởi tạo đường dẫn tới thư mục "wwwroot/images"
            _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            // Kiểm tra nếu thư mục chưa tồn tại thì tạo mới
            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            // Lấy tên file từ file được upload
            var fileName = Path.GetFileName(file.FileName);
            // Xây dựng đường dẫn lưu file
            var filePath = Path.Combine(_uploadFolder, fileName);

            // Mở một FileStream để ghi file vào đường dẫn đã chỉ định
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageUrl = $"/images/{fileName}";
            return Ok(new { imageUrl });
        }
    }
}
