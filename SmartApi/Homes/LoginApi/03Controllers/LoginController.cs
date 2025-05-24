using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmartApi.Commons.EncodeDecode;
using SmartApi.Homes.Employees._01Models;
using SmartApi.Homes.LoginApi._01Models;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SmartApi.Homes.LoginApi._03Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginServices _OBJ;
        private IConfiguration _config;
        private readonly IEncodeDecode _envdec;
        public LoginController(ILoginServices _OBJ , IConfiguration _config, IEncodeDecode _envdec) { 
            this._OBJ = _OBJ;
            this._config = _config;
            this._envdec = _envdec;

        }
        [HttpPost]
        public IActionResult LoginService([FromBody] LoginRequest req1)
        {

            try
            {
                string key = _config.GetSection("EncrDecrKey").GetSection("JWTKey").Value;
                var issuer = "http://localhost:3000";//_config.GetSection("issuers").GetSection("issuer").Value;
                var Audience = "http://localhost:3000";//_config.GetSection("issuers").GetSection("issuer").Value;
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var prmClaims = new List<Claim>();
                prmClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                Login req = new Login();
                req.UserId = req1.UserID;
                req.Password = req1.Password;
                var key1 = _config.GetSection("EncrDecrKey").GetSection("EncDecComKey").Value;
                var result = _OBJ._ILoginServices(req);
                var tokens = "";
                if (result.ToList<Login>().Count > 0)
                {
                    foreach (var res in result)
                    {
                            prmClaims.Add(new Claim("UserId", res.UserId ?? ""));
                           // prmClaims.Add(new Claim("Password", res.Password ?? ""));
                        prmClaims.Add(new Claim("Password", _envdec._SCOMMEncryptString(key1, res.Password.ToString()) ?? ""));
                    }

                    var token = new JwtSecurityToken(issuer, issuer, prmClaims, expires: DateTime.Now.AddDays(1),
                        signingCredentials: credentials
                        );

                    tokens = new JwtSecurityTokenHandler().WriteToken(token);

                }
                else
                {
                    tokens = "";
                }
                return new OkObjectResult(tokens);


            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

    }
    public class LoginRequest
    {
        [Required]
        public string UserID { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
