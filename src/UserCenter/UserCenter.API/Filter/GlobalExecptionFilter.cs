using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using UserCenter.API.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace UserCenter.API.Filter
{
    public class GlobalExecptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;

        public GlobalExecptionFilter(IHostingEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var ErrorResult = new JsonErrorResult();
            if (context.Exception.GetType() == typeof(ValidatorException))
            {
                ErrorResult.Message = context.Exception.Message;
                context.Result = new BadRequestObjectResult(ErrorResult);
            }
            else
            {
                ErrorResult.Message = context.Exception.Message;
                if (_env.IsDevelopment())
                {
                    ErrorResult.DevelopmentMessage = context.Exception.StackTrace;
                }
                context.Result = new InternalErrorObjectResult(ErrorResult);
            }
            context.ExceptionHandled = true;
        }
    }

    /// <summary>
    /// 内部未知错误
    /// </summary>
    public class InternalErrorObjectResult : ObjectResult
    {
        public InternalErrorObjectResult(object obj):base(obj)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
