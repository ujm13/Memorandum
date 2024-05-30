using Memorandum.Service.Exceptions;
using Memorandum.WebApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Memorandum.WebApplication.infrastructure.ExceptionFilters
{
    /// <summary>
    /// MemorandumExceptionFilter
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class MemorandumExceptionFilter: ExceptionFilterAttribute
    {
        /// <summary>
        /// OnExceptionAsync
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is MemorandumException)
            {
                context.Result = new BadRequestObjectResult(new ResultViewModel<bool>
                {
                    StatuesCode = 400,
                    StatusMessage = context.Exception.Message,
                    Data = false
                });
            }
            return base.OnExceptionAsync(context);

        }
    }
}
