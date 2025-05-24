using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartApi.Homes.ChangePasswords._01Models;

namespace SmartApi.Homes.ChangePasswords._03Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangePasswordController : ControllerBase
    {
        private IChangePasswordServices _OBJ;
        private IConfiguration _config;
        public ChangePasswordController(IChangePasswordServices _OBJ, IConfiguration _config)
        {
            this._OBJ = _OBJ;
            this._config = _config;
        }
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordRequest req1)
        {
            try
            {
                ChangePasswordRequest req = new ChangePasswordRequest();
                req.UserId = req1.UserId;
                req.OldPassword = req1.OldPassword;
                req.NewPassword = req1.NewPassword;
                req.ConfirmPassword = req1.ConfirmPassword;
                var result = _OBJ._IChangePasswordServices(req);
                return new OkObjectResult(result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
