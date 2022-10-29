using Shop.API.Setup.Gateways.Zibal.DTOs;

namespace Shop.API.Setup.Gateways.Zibal;

public interface IZibalService
{
    Task<string> StartPay(ZibalPaymentRequest request);
    Task<ZibalVerifyResponse> Verify(ZibalVerifyRequest request);
}