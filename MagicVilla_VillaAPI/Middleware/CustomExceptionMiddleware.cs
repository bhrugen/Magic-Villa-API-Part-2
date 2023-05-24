using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace MagicVilla_VillaAPI.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public CustomExceptionMiddleware(RequestDelegate requestDelegate)
        {
                _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //continue the request
                await _requestDelegate(context);
            }
            catch(Exception e)
            {
                await ProcessException(context, e);
            }
        }

        private async Task ProcessException(HttpContext httpContext, Exception e)
        {
            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType = "application/json";
                if (e is BadImageFormatException badImageException)
                {
                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new
                    {
                        //if you had custom exception where you are passing status code then you can pass here
                        StatusCode = 776,
                        ErrorMessage = "Hello From Middleware! Image Format is invalid - Finale"
                    }));
                }
                else
                {

                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new
                    {
                        StatusCode = httpContext.Response.StatusCode,
                        ErrorMessage = "Hello From Middleware! - Finale"
                    }));
                }
        }
    }
}
