using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("ErrorHandling")]
    [ApiController]
    [AllowAnonymous]
    [ApiVersionNeutral]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorHandlingController : ControllerBase
    {
        [Route("ProcessError")]
        public ActionResult ProcessError() => Problem();

        [Route("ProcessErrorDev")]
        public IActionResult ProcessErrorDev([FromServices] IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment.IsDevelopment())
            {
                var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();

                return Problem(
                    detail: feature.Error.InnerException?.StackTrace,
                    title: feature.Error.Message,
                    instance: "Development"
                    );
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
