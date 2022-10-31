using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.OrderAggregate;
using Shop.Query.Orders._DTOs;
using Shop.UI.Services.Orders;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Profile.Orders;

public class IndexModel : BaseRazorPage
{
    private readonly IOrderService _orderService;

    public IndexModel(IRazorToStringRenderer razorToStringRenderer,
        IOrderService orderService) : base(razorToStringRenderer)
    {
        _orderService = orderService;
    }

    public OrderFilterResult? Orders { get; set; }

    public async Task OnGet(int pageId = 1, Order.OrderStatus? status = null)
    {
        Orders = await GetData(async () => await _orderService.GetByFilterForUser(pageId, 30, status));
    }
}