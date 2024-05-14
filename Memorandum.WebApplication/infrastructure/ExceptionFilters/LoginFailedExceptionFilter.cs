using Memorandum.Service.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Memorandum.WebApplication.infrastructure.ExceptionFilters
{
    public class LoginFailedExceptionFilter: ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is LoginFailedException)
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
            }

            return base.OnExceptionAsync(context);
        }
         
    }
}
