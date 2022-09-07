using System.Net;
using Common.Api.Utility;
using Common.Application.Exceptions;
using Common.Domain.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Common.Api.Middleware;

public static class ApiCustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseApiCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ApiCustomExceptionHandlerMiddleware>();
    }
}

public class ApiCustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _environment;

    public ApiCustomExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment environment)
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