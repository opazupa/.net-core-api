using System;
using System.Net;
using System.Threading.Tasks;
using CoreLibrary.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace API.Middlewares
{
    /// <summary>
    /// Exception handler middleware.
    /// </summary>
    public class ExceptionHandlerMiddleware
    {

        private readonly RequestDelegate next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handle user thrown error types.
        /// </summary>
        /// <returns></returns>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode code;
            if (ex is BadRequestException)
            {
                code = HttpStatusCode.BadRequest;
            }
            else if (ex is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }
            else if (ex is UnauthorizedException)
            {
                code = HttpStatusCode.Unauthorized;
            }
            else
            {
                code = HttpStatusCode.InternalServerError;
            }

            var result = JsonConvert.SerializeObject(new { error = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
