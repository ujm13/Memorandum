using Memorandum.Service.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Memorandum.WebApplication.infrastructure.ExceptionFilters
{
    public class MemberNotFoundExceptionFilter: ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context) 
        {
            if (context.Exception is MemberNotFoundException)
            {
                context.Result=new NotFoundObjectResult(context.Exception.Message);
            }
            return base.OnExceptionAsync(context);
        }
    }
}
