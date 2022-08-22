using ClientPortal.Core.Application.External.Sharepoint;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientPortal.Presentation.WebApi.Controllers.v1
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ISharepointService _sharepointService;
        public ClientController(ISharepointService sharepointService)
        {
            _sharepointService = sharepointService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _sharepointService.GetClientsFromSharePoint();
            return Ok(clients);
        } 


    }
   
}
