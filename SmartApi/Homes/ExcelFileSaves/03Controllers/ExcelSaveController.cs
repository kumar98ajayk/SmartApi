using Microsoft.AspNetCore.Mvc;
using SmartApi.Homes.Employees._01Models; // Assuming this namespace contains IEmployeServices
using SmartApi.Homes.ExcelFileSaves._01Models; // Assuming this namespace contains ExcelSaveRequest

namespace SmartApi.Homes.ExcelFileSaves._03Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelSaveController : ControllerBase
    {
        private readonly IExecelSaveServices _execelSaveServices;
        private readonly IConfiguration _config;

        // Constructor injection for services and configuration
        public ExcelSaveController(IExecelSaveServices execelSaveServices, IConfiguration config)
        {
            _execelSaveServices = execelSaveServices;
            _config = config;
        }

        [HttpPost("ExcelSave")]
        public IActionResult ExcelSave([FromBody] ExcelSaveRequest request)
        {
            try
            {
                var result = _execelSaveServices._IExecelSaveServices(request); // Ensure this method matches your service definition
                return Ok(result); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("DownloadFile/{fileName}")]
        public IActionResult DownloadFile(string fileName)
        {
            try
            {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "ExcelFiles");
                string filePath = Path.Combine(folderPath, fileName);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound(new { Message = "File not found." });
                }

                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // Correct MIME type

                return File(fileBytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while downloading the file.", Details = ex.Message });
            }
        }

    }

}
