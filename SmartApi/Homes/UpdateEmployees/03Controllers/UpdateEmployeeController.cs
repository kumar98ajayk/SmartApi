using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartApi.Homes.RegistrationForms._01Models;
using SmartApi.Homes.UpdateEmployees._01Models;

namespace SmartApi.Homes.UpdateEmployees._03Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateEmployeeController : ControllerBase
    {
        private IUpdateServices _OBJ;
        private IConfiguration _config;
        public UpdateEmployeeController(IUpdateServices _OBJ, IConfiguration _config)
        {
            this._OBJ = _OBJ;
            this._config = _config;
        }
        [HttpPost("UpdateEmployee")]
        public IActionResult UpdateEmployee([FromBody] UpdateRequest req1)
        {
            try
            {
                UpdateRequest req = new UpdateRequest();
                req.id = req1.id;
                req.Name = req1.Name;
                req.lastName = req1.lastName;
                req.Emailid = req1.Emailid;
                req.mobileNo = req1.mobileNo;
                req.age = req1.age;
                req.gender = req1.gender;
                req.AadharNo = req1.AadharNo;
                req.panno = req1.panno;
                var result = _OBJ._IUpdateServices(req);
                return new OkObjectResult(result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
