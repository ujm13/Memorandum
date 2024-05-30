using Memorandum.Service.Exceptions;
using Memorandum.WebApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Memorandum.WebApplication.infrastructure.ExceptionFilters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class MemberNotFoundExceptionFilter: ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context) 
        {
            if (context.Exception is MemberNotFoundException)
            {
                context.Result=new NotFoundObjectResult(new ResultViewModel<bool>
                {
                    StatuesCode = 404,
                    StatusMessage = context.Exception.Message,
                    Data = false
                });
            }
            return base.OnExceptionAsync(context);
        }
    }
}
