using ClientPortal.Core.Application.External.Sharepoint;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientPortal.Presentation.WebApi.Controllers.v1
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ISharepointService _sharepointService;
        public ProjectController(ISharepointService sharepointService)
        {
            _sharepointService = sharepointService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _sharepointService.GetAllProjectsFromSharePoint();
            return Ok(projects);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjectsOfClient(int clientId)
        {
            var projects = await _sharepointService.GetProjectsFromSharePoint(clientId);
            return Ok(projects);
        }
    }
   
}
