using Common.Api.Utility;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc;
using Shop.Query.Orders._DTOs;
using Shop.UI.Services.Orders;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Profile.Orders;

public class ShowModel : BaseRazorPage
{
    private readonly IOrderService _orderService;

    public ShowModel(IRazorToStringRenderer razorToStringRenderer,
        IOrderService orderService) : base(razorToStringRenderer)
    {
        _orderService = orderService;
    }

    public OrderDto Order { get; set; }

    public async Task<IActionResult> OnGet(long id)
    {
        var order = await GetData(async () => await _orderService.GetById(id));
        if (order.UserId != User.GetUserId())
        {
            MakeErrorAlert(ValidationMessages.FieldInvalid("سفارش"));
            return RedirectToPage("Index");
        }
        Order = order;
        return Page();
    }
}