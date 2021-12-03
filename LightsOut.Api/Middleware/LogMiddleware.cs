using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using LightsOut.Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LightsOut.Api.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogMiddleware> _logger;

        public LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation($"{context.Request.Path} called.");
            
            var originalBodyStream = context.Response.Body;
            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured. Error : {ex}");
                await HandleExceptionAsync(context, ex);
            }
                
            await LogOutgoingResponse(context);
            await responseBody.CopyToAsync(originalBodyStream);
        }
        
        private async Task LogOutgoingResponse(HttpContext httpContext)
        {
            var responseBody = await GetResponseBody(httpContext.Response);
            _logger.LogInformation($"{httpContext.TraceIdentifier}-{httpContext.Request.Path}-{responseBody}");
        }
        
        private async Task<string> GetResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }
        
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = new ServiceResponseModel<object>()
            {
                Header = new ServiceResponseHeader()
                {
                    Message = ex.Message,
                    StatusCode = httpContext.Response.StatusCode
                }
            }; 
            await httpContext.Response.WriteAsJsonAsync(response);
        }
    }
}