using System.Net.Mime;
using System.Text;
using Common.Application.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shop.API.Setup.Gateways.Zibal.DTOs;

namespace Shop.API.Setup.Gateways.Zibal;

public class ZibalService : IZibalService
{
    private readonly HttpClient _httpClient;

    private static JsonSerializerSettings JsonSettings => new()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver()
    };

    public ZibalService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> StartPay(ZibalPaymentRequest request)
    {
        var body = JsonConvert.SerializeObject(request, JsonSettings);
        var content = new StringContent(body, Encoding.UTF8, MediaTypeNames.Application.Json);
        var result = await _httpClient.PostAsync(ZibalUrls.RequestUrl, content);
        if (result.IsSuccessStatusCode)
        {
            var response = await result.Content.ReadFromJsonAsync<ZibalPaymentResult>();
            if (response!.Result == 100)
                return $"{ZibalUrls.PaymentUrl}{response.TrackId}";

            throw new InvalidCommandApplicationException(ZibalTranslator.TranslateResult(response!.Result));
        }
        throw new InvalidCommandApplicationException(result.StatusCode.ToString());
    }

    public async Task<ZibalVerifyResponse> Verify(ZibalVerifyRequest request)
    {
        var body = JsonConvert.SerializeObject(request, JsonSettings);
        var content = new StringContent(body, Encoding.UTF8, MediaTypeNames.Application.Json);
        var result = await _httpClient.PostAsync(ZibalUrls.VerifyUrl, content);
        if (result.IsSuccessStatusCode)
        {
            return await result.Content.ReadFromJsonAsync<ZibalVerifyResponse>();
        }
        throw new InvalidCommandApplicationException(result.StatusCode.ToString());
    }
}