using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartApi.Homes.RegistrationForms._01Models;
using SmartApi.Homes.SingUps._01Models;

namespace SmartApi.Homes.SingUps._03Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private ISignupServices _OBJ;
        private IConfiguration _config;
        public SignupController(ISignupServices _OBJ, IConfiguration _config)
        {
            this._OBJ = _OBJ;
            this._config = _config;
        }
        [HttpPost("SignupP")]
        public IActionResult SignupP([FromBody] SingupReq req1)
        {
            try
            {
                SingupReq req = new SingupReq();
                req.UserId = req1.UserId;
                req.UserName = req1.UserName;
                req.EmailId = req1.EmailId;
                req.Password = req1.Password;
                var result = _OBJ._ISignupServices(req);
                return new OkObjectResult(result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
