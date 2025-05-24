using Newtonsoft.Json; // Include this using directive for JSON parsing
using OfficeOpenXml;
using SmartApi.Commons;
using SmartApi.Homes.ExcelFileSaves._01Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace SmartApi.Homes.ExcelFileSaves._02Repositorys
{
    public class ExcelSaveRepository : IExecelSaveServices
    {
        private readonly IConfiguration _config;
        private readonly IConnStringServices _conStar;
        public ExcelSaveRepository(IConfiguration config, IConnStringServices conStar)
        {
            _config = config;
            _conStar = conStar;
        }

        public object _IExecelSaveServices(ExcelSaveRequest request)
        {
            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "ExcelFiles");

            try
            {
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                using (SqlConnection con = new SqlConnection(_conStar.strConn()))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("[dbo].[SP_FileSave]", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@RequestId", request.RequestId);

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                string jsonData = rdr["FileData"].ToString();

                                var records = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                                string fileName = $"{request.RequestId}.xlsx";
                                string filePath = Path.Combine(savePath, fileName);
                                using (ExcelPackage package = new ExcelPackage())
                                {
                                    var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                                    var headers = records.FirstOrDefault()?.Keys.ToList();
                                    if (headers != null)
                                    {
                                        for (int col = 0; col < headers.Count; col++)
                                        {
                                            worksheet.Cells[1, col + 1].Value = headers[col];
                                        }

                                        for (int row = 0; row < records.Count; row++)
                                        {
                                            var record = records[row];
                                            for (int col = 0; col < headers.Count; col++)
                                            {
                                                var key = headers[col];
                                                worksheet.Cells[row + 2, col + 1].Value = record[key];
                                            }
                                        }
                                    }
                                    FileInfo excelFile = new FileInfo(filePath);
                                    package.SaveAs(excelFile);
                                }

                                return new { Message = "Excel file saved successfully.", FileName = fileName, FilePath = filePath };
                            }
                            else
                                return new { Error = "No data found for the provided RequestId." };
                            }
                        }
                    }
                
            }
            catch (Exception ex)
            {
                return new { Error = "An error occurred while saving the file.", Details = ex.Message };
            }
        }
    }
}
