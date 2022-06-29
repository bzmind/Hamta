using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace Shop.UI.SetupClasses.CustomAttributes;

// Doesn't work
public class RateLimitFilter : IPageFilter
{
    private readonly IMemoryCache _memoryCache;
    private readonly string _nullValue = Guid.NewGuid().ToString();
    private const int Seconds = 5;

    public void Set(string cacheKey, string toSet)
        => _memoryCache.Set<string>(cacheKey, toSet == null ? _nullValue : toSet, DateTime.Now.AddSeconds(Seconds));

    public string Get(string cacheKey)
    {
        var isInCache = _memoryCache.TryGetValue(cacheKey, out string cachedVal);
        if (!isInCache) return null;

        return cachedVal == _nullValue ? null : cachedVal;
    }

    public RateLimitFilter(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        // Using the IP Address here as part of the key but you could modify
        // and use the username if you are going to limit only authenticated users
        // filterContext.HttpContext.User.Identity.Name
        var key = string.Format($"{context.HttpContext.Connection.RemoteIpAddress}");
        var allowExecute = false;
        var value = Get(key);
        if (value == null)
        {
            Set(key, "ddddddd");
            allowExecute = true;
        }

        if (!allowExecute)
        {
            context.Result = new ContentResult
            {
                Content = $"You can call this every {Seconds} seconds"
            };
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
        }
    }

    public void OnPageHandlerSelected(PageHandlerSelectedContext context) { }

    public void OnPageHandlerExecuted(PageHandlerExecutedContext context) { }
}