using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace MagicVilla_VillaAPI.Extensions
{
    public static class CustomExceptionHandler
    {
        public static void HandleError(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = 777;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        if(contextFeature.Error is BadImageFormatException badImageException)
                        {
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                            {
                                //if you had custom exception where you are passing status code then you can pass here
                                StatusCode = 776,
                                ErrorMessage = "Hello From Middleware! Image Format is invalid"
                            }));
                        }
                        else
                        {
                            
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            StatusCode = context.Response.StatusCode,
                            ErrorMessage = "Hello From Middleware!"
                        }));
                        }

                    }
                });
            });
        }
    }
}
