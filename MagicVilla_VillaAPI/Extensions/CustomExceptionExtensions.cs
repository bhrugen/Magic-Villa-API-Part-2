using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace MagicVilla_VillaAPI.Extensions
{
    public static class CustomExceptionExtensions
    {
        public static void HandleError(this IApplicationBuilder app, bool isDevelopment) 
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var feature = context.Features.Get<IExceptionHandlerFeature>();
                    if (feature != null)
                    {
                        if (isDevelopment)
                        {
                            if (feature.Error is BadImageFormatException badImageException)
                            {
                                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                                {
                                    //if you had custom exception where you are passing status code then you can pass here
                                    StatusCode = 776,
                                    ErrorMessage = "Hello From Custom Handler! Image Format is invalid"
                                }));
                            }
                            else
                            {
                                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                                {
                                    StatusCode = context.Response.StatusCode,
                                    ErrorMessage = feature.Error.Message,
                                    StackTrace = feature.Error.StackTrace
                                }));
                            }
                        }
                        else
                        {
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                            {
                                Statuscode = context.Response.StatusCode,
                                ErrorMessage = "Hello From Program.cs Exception Handler"
                            }));
                        }
                    }

                });
            });
        }
    }
}
