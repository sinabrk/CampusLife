using BG.CampusLife.Domain.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BG.CampusLife.Presentation.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var result = string.Empty;

            switch (exception)
            {
                case CampusException campusException:
                    code = (HttpStatusCode)campusException.Code;
                    result = JsonConvert.SerializeObject(new { error = campusException.Error });
                    break;
                
                case NullReferenceException nullReferenceException:
                    code = HttpStatusCode.Forbidden;
                    result = JsonConvert.SerializeObject(new
                    {
                        error = nullReferenceException.Message,
                        src = nullReferenceException.Source,
                    });
                    break;
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(new
                    { error = validationException.Message, failures = validationException.Failures });
                    break;
                case BadRequestException badRequestException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(new { error = badRequestException.Message });
                    break;
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
                case AuthenticationException authenticationException:
                    code = HttpStatusCode.Forbidden;
                    result = JsonConvert.SerializeObject(new { error = authenticationException.Error });
                    break;
                case InvalidTokenException invalidTokenException:
                    code = HttpStatusCode.Unauthorized;
                    result = invalidTokenException.Message;
                    break;
                case MultipleErrorBadException multipleErrorBadException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(new
                    { message = multipleErrorBadException.Message, errors = multipleErrorBadException.Errors });
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == string.Empty)
            {
                result = JsonConvert.SerializeObject(new { error = exception.Message + exception.InnerException?.Message ?? string.Empty });
            }

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}