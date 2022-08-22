using ClientPortal.Core.Application.DTOs;
using ClientPortal.Core.Application.External.Sharepoint;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientPortal.Presentation.WebApi.Controllers.v1
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISharepointService _sharepointService;
        public UserController(ISharepointService sharepointService)
        {
            _sharepointService = sharepointService;
        }
       
        [HttpPost]
        public async Task<IActionResult> AddProjectUser(SharepointUserProjectData data)
        {
            var results = await _sharepointService.AddProjectUserSharePoint(data);
            if (results != null)
                return Ok(results);
            return BadRequest("Error executing the post");
        }

        [HttpPost]
        public async Task<IActionResult> AddProjectsUser(SharepointUserProjectsData data)
        {
            var results = await _sharepointService.AddProjectsUserSharePoint(data);
            if (results != null)
                return Ok(results);
            return BadRequest("Error executing the post");
        }

        [HttpGet]
        public async Task<IActionResult> GetMemberFirmUsers()
        {
            var results = await _sharepointService.GetMemberFirmUsersSharePoint();
            return Ok(results);
        }

    }

}
