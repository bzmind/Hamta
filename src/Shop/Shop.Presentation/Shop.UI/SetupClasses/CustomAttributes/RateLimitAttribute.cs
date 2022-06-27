using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace Shop.UI.SetupClasses.CustomAttributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class RateLimitAttribute : ActionFilterAttribute
{
    private readonly IMemoryCache _memoryCache;

    public RateLimitAttribute(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public int Seconds { get; set; }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        // Using the IP Address here as part of the key but you could modify
        // and use the username if you are going to limit only authenticated users
        // filterContext.HttpContext.User.Identity.Name
        var key = string.Format($"{filterContext.HttpContext.Connection.RemoteIpAddress}");
        var allowExecute = false;

        if (_memoryCache.TryGetValue(key, out string k) == false)
        {
            _memoryCache.Set(key, true, DateTime.Now.AddSeconds(Seconds));
            allowExecute = true;
        }

        if (!allowExecute)
        {
            filterContext.Result = new ContentResult
            {
                Content = $"You can call this every {Seconds} seconds"
            };
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
        }
    }
}