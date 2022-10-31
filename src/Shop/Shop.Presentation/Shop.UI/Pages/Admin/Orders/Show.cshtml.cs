using Shop.Query.Orders._DTOs;
using Shop.UI.Services.Orders;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Admin.Orders;

public class ShowModel : BaseRazorPage
{
    private readonly IOrderService _orderService;

    public ShowModel(IRazorToStringRenderer razorToStringRenderer,
        IOrderService orderService) : base(razorToStringRenderer)
    {
        _orderService = orderService;
    }

    public OrderDto Order { get; set; }

    public async Task OnGet(long orderId)
    {
        Order = await GetData(async () => await _orderService.GetById(orderId));
    }
}