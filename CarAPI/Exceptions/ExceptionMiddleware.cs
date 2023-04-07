using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Doamin.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        //private ILoggerManager _logger;

        public ExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext httpContext/*, ILoggerManager logger*/)
        {
            //_logger = logger;

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var requestString = await GetRequestString(httpContext.Request);
                var userAgent = httpContext.Request.Headers["User-Agent"].ToString() ?? string.Empty;

                //await _logger.LogExceptionAsync(ex, requestString, userAgent);

                await HandleExceptionAsync(httpContext);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error."
            }.ToString());
        }

        private static async Task<string> GetRequestString(HttpRequest request)
        {
            var body = request.Body;


            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }
    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
