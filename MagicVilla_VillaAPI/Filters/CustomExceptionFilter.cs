using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MagicVilla_VillaAPI.Filters
{
    public class CustomExceptionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Exception is FileNotFoundException fileNotFoundException)
            {
                context.Result = new ObjectResult("File Not found but handled in filter")
                {
                    StatusCode = 503
                };
                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
