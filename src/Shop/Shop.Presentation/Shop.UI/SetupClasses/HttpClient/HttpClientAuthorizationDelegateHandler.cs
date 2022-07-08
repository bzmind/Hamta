namespace Shop.UI.SetupClasses.HttpClient;

public class HttpClientAuthorizationDelegateHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpClientAuthorizationDelegateHandler(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_contextAccessor.HttpContext == null)
            return await base.SendAsync(request, cancellationToken);

        var token = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

        if (!string.IsNullOrWhiteSpace(token))
            request.Headers.Add("Authorization", token);

        return await base.SendAsync(request, cancellationToken);
    }
}