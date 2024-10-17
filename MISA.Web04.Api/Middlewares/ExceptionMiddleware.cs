using MISA.Web04.Core.Exceptions;
using System.Net;
using MISA.Web04.Core.Resources.Employee;

namespace MISA.Web04.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
       
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex) { 
         
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Xử lý ngoại lệ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns>ngoại lệ tương ứng</returns>
        /// Created by: ttanh (30/06/2023)
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        { 
            context.Response.ContentType = "application/json";
            if(exception is ValidateException validateException) 
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
               
                await context.Response.WriteAsync(new BaseException()
                {
                    ErrorCode = context.Response.StatusCode,
                    UserMsg = exception.Message,
                    DevMsg = exception.Message,
                    TraceId = context.TraceIdentifier,
                    ErrorMsgs = validateException.ErrorMsgs,
                    Data = validateException.Data
                }.ToString() ?? "") ;
            } else if (exception is RelatedDataException relatedDataException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsync(new BaseException()
                {
                    ErrorCode = context.Response.StatusCode,
                    UserMsg = exception.Message,
                    DevMsg = exception.Message,
                    TraceId = context.TraceIdentifier,
                    ErrorMsgs = relatedDataException.ErrorMsgs
                }.ToString() ?? "");
            }
            else if (exception is NotFoundException notFoundException) 
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsync(new BaseException()
                {
                    ErrorCode = context.Response.StatusCode,
                    UserMsg = exception.Message,
                    DevMsg = exception.Message,
                    TraceId = context.TraceIdentifier,
                    ErrorMsgs = notFoundException.ErrorMsgs
                }.ToString() ?? "");
            }

            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(new BaseException()
                {
                    ErrorCode = context.Response.StatusCode,
                    UserMsg = EmployeeVN.SYSTEM_ERROR,
                    DevMsg = exception.Message,
                    TraceId = context.TraceIdentifier,
                    ErrorMsgs = new Dictionary<string, string>() { { "System", EmployeeVN.SYSTEM_ERROR } }
                }.ToString() ?? "");
            }
        }
    }
}
