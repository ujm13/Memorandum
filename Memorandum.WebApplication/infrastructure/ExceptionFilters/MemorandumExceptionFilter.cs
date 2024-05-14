﻿using Memorandum.Service.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Memorandum.WebApplication.infrastructure.ExceptionFilters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class MemorandumExceptionFilter: ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is MemorandumException)
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
            }
            return base.OnExceptionAsync(context);

        }
    }
}
