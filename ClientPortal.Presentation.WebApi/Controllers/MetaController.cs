using ClientPortal.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace ClientPortal.WebApi.Controllers
{
    public class MetaController : BaseApiController
    {
        [HttpGet("/info")]
        public ActionResult<Response<string>> Info()
        {
            return Ok(new Response<string>($"V{AppVersion}", null));
        }
    }
}
