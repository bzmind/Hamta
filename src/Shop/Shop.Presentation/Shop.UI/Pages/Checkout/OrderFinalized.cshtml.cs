using Common.Api.Utility;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc;
using Shop.Query.Orders._DTOs;
using Shop.UI.Services.Orders;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Checkout;

public class OrderFinalizedModel : BaseRazorPage
{
    private readonly IOrderService _orderService;

    public OrderFinalizedModel(IRazorToStringRenderer razorToStringRenderer,
        IOrderService orderService) : base(razorToStringRenderer)
    {
        _orderService = orderService;
    }

    public OrderDto Order { get; set; }

    public async Task<IActionResult> OnGet(long orderId)
    {
        Order = await GetData(async () => await _orderService.GetById(orderId));
        if (Order == null || Order.UserId != User.GetUserId())
        {
            MakeErrorAlert(ValidationMessages.FieldNotFound("سفارش"));
            return Redirect("/");
        }
        return Page();
    }
}