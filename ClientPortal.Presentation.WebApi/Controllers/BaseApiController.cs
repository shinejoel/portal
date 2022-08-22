using ClientPortal.Presentation.WebApi;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ClientPortal.WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices?.GetService<IMediator>();
        // Replace with azure AD Auth
        //public CurrentUser CurrentUser => (CurrentUser)HttpContext.Items["CurrentUser"];

        #region Helper methods 

        protected string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        protected StringValues GetOrigin()
        {
            return Request.Headers["origin"];
        }

        protected async Task<IActionResult> SendCommand<T>(T command)
        {
            return Ok(await Mediator.Send(command));
        }

        protected void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append($"BDO_Outperform-{AppVersion}", token, cookieOptions);
        }

        protected void DeleteTokenCookie()
        {
            Response.Cookies.Delete($"BDO_Outperform-{AppVersion}");
        }

        protected void DeleteTokenCookie(string cookieKey)
        {
            Response.Cookies.Delete(cookieKey);
        }

        protected string AppVersion => FileVersionInfo.GetVersionInfo(typeof(Startup).Assembly.Location).ProductVersion;

        #endregion
    }
}
