using AspNetCoreRateLimit;
using Common.Api;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shop.API.SetupClasses;

public class CustomIpRateLimitMiddleware : IpRateLimitMiddleware
{
    public CustomIpRateLimitMiddleware(RequestDelegate next, IProcessingStrategy processingStrategy,
        IOptions<IpRateLimitOptions> options, IIpPolicyStore policyStore, IRateLimitConfiguration config,
        ILogger<IpRateLimitMiddleware> logger) : base(next, processingStrategy, options, policyStore, config, logger)
    {
    }

    public override async Task ReturnQuotaExceededResponse(HttpContext httpContext, RateLimitRule rule,
        string retryAfter)
    {
        var result = new ApiResult
        {
            IsSuccessful = false,
            MetaData = new MetaData
            {
                ApiStatusCode = ApiStatusCode.TooManyRequests,
                Message = "بیش از حد مجاز تلاش کرده‌اید"
            }
        };

        var jsonOptions = new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } };
        var jsonResult = JsonSerializer.Serialize(result, jsonOptions);
        httpContext.Response.Headers["Retry-After"] = retryAfter;
        httpContext.Response.StatusCode = (int)ApiStatusCode.TooManyRequests;
        httpContext.Response.ContentType = "application/json";

        await httpContext.Response.WriteAsync(jsonResult);
        //return base.ReturnQuotaExceededResponse(httpContext, rule, retryAfter);
    }
}