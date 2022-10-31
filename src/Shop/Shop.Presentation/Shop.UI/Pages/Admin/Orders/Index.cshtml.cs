using Microsoft.AspNetCore.Mvc;
using Shop.Infrastructure.Utility;
using Shop.Query.Orders._DTOs;
using Shop.UI.Services.Orders;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Admin.Orders;

public class IndexModel : BaseRazorPage
{
    private readonly IOrderService _orderService;

    public IndexModel(IRazorToStringRenderer razorToStringRenderer,
        IOrderService orderService) : base(razorToStringRenderer)
    {
        _orderService = orderService;
    }

    [BindProperty(SupportsGet = true)]
    public OrderFilterParams FilterParams { get; set; }
    public OrderFilterResult Orders { get; set; }

    public async Task OnGet(string? startDate, string? endDate)
    {
        if (!string.IsNullOrWhiteSpace(startDate))
            FilterParams.StartDate = startDate.ToMiladi();

        if (!string.IsNullOrWhiteSpace(endDate))
            FilterParams.EndDate = endDate.ToMiladi();

        Orders = await GetData(async () => await _orderService.GetByFilter(FilterParams));
    }
}