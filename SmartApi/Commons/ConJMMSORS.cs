using Microsoft.Extensions.Configuration;

namespace SmartApi.Commons
{
    public class ConJMMSORS : IConnStringServices
    {

        private readonly IConfiguration _config;
        public ConJMMSORS(IConfiguration _config)
        {
            this._config = _config;
        }

        public string strConn()
        {
            string strConn = _config.GetSection("ConnectionStrings").GetSection("connStr").Value;
            return strConn;
        }
        public string JsonFilePath()
        {
            return @"C:\path\file.json";
            //return _config.GetValue<string>("JsonFilePath");
        }
    }
}
