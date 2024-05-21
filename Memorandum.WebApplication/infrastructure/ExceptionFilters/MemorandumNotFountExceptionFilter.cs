using Memorandum.Service.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Memorandum.WebApplication.infrastructure.ExceptionFilters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class MemorandumNotFountExceptionFilter: ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is MemorandumNotFountException)
            {
                context.Result = new NotFoundObjectResult(context.Exception.Message);
            }
            return base.OnExceptionAsync(context);
        }
    }
}
