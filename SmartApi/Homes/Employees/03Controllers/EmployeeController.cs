using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SmartApi.Homes.Employees._01Models;
using System.Net;

namespace SmartApi.Homes.Employees._03Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeServices _OBJ;
        private IConfiguration _config;
        public EmployeeController(IEmployeServices _OBJ, IConfiguration _config) {
            this._OBJ = _OBJ;
            this._config = _config;
        }
        [HttpPost("EmployeeFetch")]
        public IActionResult EmployeeFetch()
        {
            try
            {
                EmployeResponse req = new EmployeResponse();
                var result = _OBJ._IEmployeServices(req);
                return new OkObjectResult(result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //[HttpPost("upload-excel")]
        //public async Task<IActionResult> UploadExcel(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //        return BadRequest("No file uploaded");

        //    if (!file.FileName.EndsWith(".xlsx"))
        //        return BadRequest("Invalid file format. Please upload an Excel file.");

        //    try
        //    {
        //        var data = new List<string>(); // Example data list to store read data

        //        using (var stream = new MemoryStream())
        //        {
        //            await file.CopyToAsync(stream);
        //            using (var package = new ExcelPackage(stream))
        //            {
        //                var worksheet = package.Workbook.Worksheets[0]; // Get the first worksheet

        //                for (int row = 2; row <= worksheet.Dimension.End.Row; row++) // Assuming first row is the header
        //                {
        //                    var value = worksheet.Cells[row, 1].Text; // Read from the first column
        //                    data.Add(value);
        //                }
        //            }
        //        }

        //        // Do something with the data (e.g., save it to a database)

        //        return Ok(new { Message = "File uploaded successfully", Data = data });
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}


     
    }
}
