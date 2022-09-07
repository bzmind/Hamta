using System.Net;
using Common.Api;
using Common.Api.Utility;
using Common.Application.Exceptions;
using Common.Domain.Exceptions;
using Newtonsoft.Json;
namespace Shop.UI.Setup.Middleware;

public static class UiCustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseUiCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UiCustomExceptionHandlerMiddleware>();
    }
}

public class UiCustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _environment;

    public UiCustomExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment environment)
    {
        _next = next;
        _environment = environment;
    }

    public async Task Invoke(HttpContext context)
    {
        string? exceptionMessage = null;
        var httpStatusCode = HttpStatusCode.InternalServerError;
        var apiStatusCode = ApiStatusCode.ServerError;

        try
        {
            await _next(context);
        }
        catch (InvalidDataDomainException exception)
        {
            SetErrorMessage(exception);
            await WriteToResponseAsync();
        }
        catch (InvalidCommandApplicationException exception)
        {
            apiStatusCode = ApiStatusCode.BadRequest;
            httpStatusCode = HttpStatusCode.BadRequest;
            SetErrorMessage(exception);
            await WriteToResponseAsync();
        }
        catch (Exception exception)
        {
            SetErrorMessage(exception);
            await WriteToResponseAsync();
        }

        void SetErrorMessage(Exception exception)
        {
            exceptionMessage = exception.Message;

            if (_environment.IsDevelopment())
            {
                exceptionMessage = exception.Message + Environment.NewLine +
                                   exception.InnerException + Environment.NewLine +
                                   exception.StackTrace;

                // This is because the browser would complaint about the cookie size, because
                // if I were to return the exceptionMessage as above, it would surpass the size limit
                if (exceptionMessage.Length > 3000)
                    exceptionMessage = exceptionMessage.Truncate(3000);
            }
        }

        async Task WriteToResponseAsync()
        {
            if (context.Response.HasStarted)
                throw new InvalidOperationException("The response has already started, " +
                                                    "the http status code middleware will not be executed.");

            if (_environment.IsDevelopment())
            {
                context.Response.StatusCode = (int)httpStatusCode;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(exceptionMessage);
                return;
            }
            
            var result = new ApiResult
            {
                IsSuccessful = false,
                MetaData = new MetaData
                {
                    ApiStatusCode = apiStatusCode,
                    Message = exceptionMessage
                }
            };

            var json = JsonConvert.SerializeObject(result, Formatting.Indented);
            context.Response.StatusCode = (int)httpStatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
    }
}