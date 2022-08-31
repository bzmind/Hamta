using System.Net;
using System.Text.Json;
using Common.Api;

namespace Shop.UI.Services;

public abstract class BaseService
{
    private readonly HttpClient _client;
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
        try
        {
            finalResult = await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
        }
        catch (Exception e)
        {
            var resultError = await result.Content.ReadAsStringAsync();
            finalResult = ApiResult.Error(resultError);
        }

        return finalResult;
    }

    private async Task<ApiResult<TData>> HandleResult<TData>(HttpResponseMessage result)
    {
        ApiResult<TData> finalResult;
        try
        {
            finalResult = await result.Content.ReadFromJsonAsync<ApiResult<TData>>(_jsonOptions);
            if (finalResult.MetaData == null)
            {
                finalResult.IsSuccessful = false;
                finalResult.MetaData = new MetaData
                {
                    ApiStatusCode = ApiStatusCode.ServerError,
                    Message = $"Status Code: {(int)result.StatusCode} {result.ReasonPhrase}"
                };
            }
        }
        catch (Exception e)
        {
            var resultError = await result.Content.ReadAsStringAsync();
            finalResult = ApiResult<TData>.Error(resultError);
        }

        return finalResult;
    }
}