using System.Collections;
using System.Text;
using System.Text.Json;
using Common.Api;

namespace Shop.UI.Services;

public abstract class BaseService
{
    private readonly HttpClient _client;
    private readonly HttpContext _httpContext;
    private readonly JsonSerializerOptions _jsonOptions;

    protected BaseService(HttpClient client, JsonSerializerOptions jsonOptions)
    {
        _client = client;
        _jsonOptions = jsonOptions;
    }

    protected abstract string ApiEndpointName { get; set; }

    protected async Task<ApiResult> PostAsJsonAsync(string endpointAction, object model)
    {
        var result = await _client.PostAsJsonAsync($"{ApiEndpointName}/{endpointAction}", model);
        return await HandleResult(result);
    }

    protected async Task<ApiResult<TData>> PostAsJsonAsync<TData>(string endpointAction, object model)
    {
        var result = await _client.PostAsJsonAsync($"{ApiEndpointName}/{endpointAction}", model);
        return await HandleResult<TData>(result);
    }

    protected async Task<ApiResult> PostAsFormDataAsync(string endpointAction, HttpContent data)
    {
        var result = await _client.PostAsync($"{ApiEndpointName}/{endpointAction}", data);
        return await HandleResult(result);
    }

    protected async Task<ApiResult<TData>> PostAsFormDataAsync<TData>(string endpointAction, HttpContent data)
    {
        var result = await _client.PostAsync($"{ApiEndpointName}/{endpointAction}", data);
        return await HandleResult<TData>(result);
    }

    protected async Task<ApiResult<TData>> PostAsync<TData>(string endpointAction)
    {
        var result = await _client.PostAsync($"{ApiEndpointName}/{endpointAction}", null);
        return await HandleResult<TData>(result);
    }

    protected async Task<ApiResult> PutAsJsonAsync(string endpointAction, object model)
    {
        var result = await _client.PutAsJsonAsync($"{ApiEndpointName}/{endpointAction}", model);
        return await HandleResult(result);
    }

    protected async Task<ApiResult> PutAsync(string endpointAction)
    {
        var result = await _client.PutAsync($"{ApiEndpointName}/{endpointAction}", null);
        return await HandleResult(result);
    }

    protected async Task<ApiResult> PutAsFormDataAsync(string endpointAction, HttpContent data)
    {
        var result = await _client.PutAsync($"{ApiEndpointName}/{endpointAction}", data);
        return await HandleResult(result);
    }

    protected async Task<ApiResult<TData>> PutAsync<TData>(string endpointAction)
    {
        var result = await _client.PutAsync($"{ApiEndpointName}/{endpointAction}", null);
        return await HandleResult<TData>(result);
    }

    protected async Task<ApiResult> DeleteAsync(string endpointAction)
    {
        var result = await _client.DeleteAsync($"{ApiEndpointName}/{endpointAction}");
        return await HandleResult(result);
    }

    protected async Task<ApiResult> DeleteAsJsonAsync(string endpointAction, object model)
    {
        var request = new HttpRequestMessage
        {
            Content = JsonContent.Create(model),
            Method = HttpMethod.Delete,
            RequestUri = new Uri($"{ApiEndpointName}/{endpointAction}", UriKind.Relative)
        };
        var result = await _client.SendAsync(request);
        return await HandleResult(result);
    }

    protected async Task<ApiResult<TData>> GetFromJsonAsync<TData>(string endpointAction)
    {
        var result = await _client.GetAsync($"{ApiEndpointName}/{endpointAction}");
        return await HandleResult<TData>(result);
    }

    private async Task<ApiResult> HandleResult(HttpResponseMessage result)
    {
        ApiResult finalResult;
        var reasonPhrase = $"Status Code: {(int)result.StatusCode} {result.ReasonPhrase}";

        try
        {
            finalResult = await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
        }
        catch (Exception e)
        {
            var resultError = await result.Content.ReadAsStringAsync();
            finalResult = ApiResult.Error
                (string.IsNullOrWhiteSpace(resultError) ? reasonPhrase : e.Message);
        }

        return finalResult;
    }

    private async Task<ApiResult<TData>> HandleResult<TData>(HttpResponseMessage result)
    {
        ApiResult<TData> finalResult;
        var reasonPhrase = $"Status Code: {(int)result.StatusCode} {result.ReasonPhrase}";

        try
        {
            finalResult = await result.Content.ReadFromJsonAsync<ApiResult<TData>>(_jsonOptions);
        }
        catch (Exception e)
        {
            var resultError = await result.Content.ReadAsStringAsync();
            finalResult = ApiResult<TData>.Error
                (string.IsNullOrWhiteSpace(resultError) ? reasonPhrase : e.Message);
        }

        return finalResult;
    }

    public string MakeQueryUrl(string endPointActionName, object obj)
    {
        var stringBuilder = new StringBuilder($"{endPointActionName}?");
        foreach (var propertyInfo in obj.GetType().GetProperties())
        {
            var propertyValue = propertyInfo.GetValue(obj, null);
            var isCollection = typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType) &&
                                   propertyInfo.PropertyType != typeof(string);
            var end = propertyInfo.Equals(obj.GetType().GetProperties().Last()) ? "" : "&";
            if (propertyValue != null && !isCollection)
                stringBuilder.Append($"{propertyInfo.Name}={propertyValue}{end}");
        }
        return stringBuilder.ToString();
    }
}