using Common.Api;
using Common.Api.Utility;
using Common.Application;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Setup.Gateways.Zibal;
using Shop.API.Setup.Gateways.Zibal.DTOs;
using Shop.API.ViewModels.Transactions;
using Shop.Presentation.Facade.Orders;

namespace Shop.API.Controllers;

[Route("api/[controller]")]
public class TransactionController : BaseApiController
{
    private readonly IZibalService _zibalService;
    private readonly IOrderFacade _orderFacade;

    public TransactionController(IZibalService zibalService, IOrderFacade orderFacade)
    {
        _zibalService = zibalService;
        _orderFacade = orderFacade;
    }

    [Authorize]
    [HttpPost]
    public async Task<ApiResult<string>> Create(CreateTransactionViewModel model)
    {
        var order = await _orderFacade.GetById(model.OrderId);
        if (order == null || order.Address == null || order.ShippingName == null)
            return CommandResult(OperationResult<string>.NotFound(ValidationMessages.FieldNotFound("سفارش")));

        var callBackUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
        var result = await _zibalService.StartPay(new ZibalPaymentRequest
        {
            Merchant = "zibal",
            Mobile = User.GetUserPhoneNumber(),
            Amount = order.TotalOrderDiscountedPrice,
            CallBackUrl = $"{callBackUrl}/api/transaction?OrderId={order.Id}" +
                          $"&errorRedirect={model.ErrorCallbackUrl}&successRedirect={model.SuccessCallbackUrl}",
            Description = $"پرداخت سفارش با شناسه {order.Id}",
            LinkToPay = false,
            SendSms = false
        });

        return CommandResult(OperationResult<string>.Success(result));
    }

    [HttpGet]
    public async Task<IActionResult> Verify(long orderId, long trackId, int success,
        string errorRedirect, string successRedirect)
    {
        if (success == 0)
            return Redirect(errorRedirect);

        var order = await _orderFacade.GetById(orderId);
        if (order == null)
            return Redirect(errorRedirect);

        var verifyResult = await _zibalService.Verify(new ZibalVerifyRequest(trackId, "zibal"));
        if (verifyResult.Result != 100)
            return Redirect(errorRedirect);

        if (verifyResult.Amount != order.TotalOrderDiscountedPrice)
            return Redirect(errorRedirect);

        var finalizeOrderResult = await _orderFacade.Finalize(orderId);
        if (finalizeOrderResult.StatusCode == OperationStatusCode.Success)
            return Redirect(successRedirect);

        return Redirect(errorRedirect);
    }
}