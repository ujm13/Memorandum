using Memorandum.Service.Exceptions;
using Memorandum.WebApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Memorandum.WebApplication.infrastructure.ExceptionFilters
{
    /// <summary>
    /// MemorandumNotFountExceptionFilter
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class MemorandumNotFoundExceptionFilter: ExceptionFilterAttribute
    {
        /// <summary>
        /// OnExceptionAsync
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is MemorandumNotFoundException)
            {
                context.Result = new NotFoundObjectResult(new ResultViewModel<bool>
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
