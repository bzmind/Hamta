using Newtonsoft.Json;

namespace Shop.API.Setup.Gateways.Zibal.DTOs;

public class ZibalPaymentRequest
{
    [JsonProperty("merchant")]
    public string Merchant { get; set; }
    public int Amount { get; set; }

    [JsonProperty("callbackUrl")]
    public string CallBackUrl { get; set; }
    public string Description { get; set; }
    public string Mobile { get; set; }
    public bool LinkToPay { get; set; }

    [JsonProperty("sms")]
    public bool SendSms { get; set; }
}