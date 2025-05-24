using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartApi.Homes.Employees._01Models;
using SmartApi.Homes.RegistrationForms._01Models;

namespace SmartApi.Homes.RegistrationForms._03Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistraionFormController : ControllerBase
    {
        private IRegistrationFormsServices _OBJ;
        private IConfiguration _config;
        public RegistraionFormController(IRegistrationFormsServices _OBJ, IConfiguration _config)
        {
            this._OBJ = _OBJ;
            this._config = _config;
        }
        [HttpPost("FranchiseeHoldingReports")]
        public IActionResult FranchiseeHoldingService([FromBody] RegistrationFormsReq req1)
        {
            try
            {
                RegistrationFormsReq req = new RegistrationFormsReq();
                req.CustomerId = req1.CustomerId;
                req.Name =req1.Name;
                req.lastName = req1.lastName;
                req.Emailid=req1.Emailid;
                req.mobileNo = req1.mobileNo;
                req.age = req1.age;
                req.gender = req1.gender;
                req.AadharNo=req1.AadharNo;
                req.panno = req1.panno;
                var result = _OBJ._IRegistrationFormsServices(req);
                return new OkObjectResult(result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
