using IncidentPrioritization.Interfaces;
using IncidentPrioritization.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IncidentPrioritization.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ITSMController : ControllerBase
    {
        private readonly IITSMService _itsmService;
        public ITSMController(IITSMService itsmService)
        {
            _itsmService = itsmService;
        }


        [Route("/api/request-handler")]
        [HttpPost]
        public async Task<ActionResult> RequestHandler([FromBody] Request request)
        {
            return Ok(await _itsmService.ITSMRequest(request.Login, request.RequestId));
        }
    }
}
