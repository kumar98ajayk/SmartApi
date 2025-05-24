using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartApi.Homes.ClientDetails._01Models;

namespace SmartApi.Homes.ClientDetails._03Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientDetailsController : ControllerBase
    {
        private readonly IClientDetailsServices _clientDetailRepository;

        public ClientDetailsController(IClientDetailsServices clientDetailRepository)
        {
            _clientDetailRepository = clientDetailRepository;
        }
        [HttpPost("GetClientStatus")]
        public ActionResult<IEnumerable<ClientDetailsResponse>> GetClientStatus([FromBody] ClientDetailsRequest request)
        {
            try
            {
                var result = _clientDetailRepository._ClientStatusModel1Services(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately (e.g., log the error)
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
