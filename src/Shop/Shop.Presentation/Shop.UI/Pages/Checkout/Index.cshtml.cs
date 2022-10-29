using Common.Api.Utility;
using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Orders;
using Shop.API.ViewModels.Transactions;
using Shop.Query.Orders._DTOs;
using Shop.Query.Shippings._DTOs;
using Shop.Query.Users._DTOs;
using Shop.UI.Services.Orders;
using Shop.UI.Services.Shippings;
using Shop.UI.Services.Transactions;
using Shop.UI.Services.UserAddresses;
using Shop.UI.Setup.ModelStateExtensions;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Checkout;

public class IndexModel : BaseRazorPage
{
    private readonly IOrderService _orderService;
    private readonly IUserAddressService _userAddressService;
    private readonly IShippingService _shippingService;
    private readonly ITransactionService _transactionService;

    public IndexModel(IRazorToStringRenderer razorToStringRenderer, IOrderService orderService,
        IUserAddressService userAddressService, IShippingService shippingService,
        ITransactionService transactionService) : base(razorToStringRenderer)
    {
        _orderService = orderService;
        _userAddressService = userAddressService;
        _shippingService = shippingService;
        _transactionService = transactionService;
    }

    public List<UserAddressDto> UserAddresses { get; set; }
    public OrderDto Order { get; set; }
    public List<ShippingDto> Shippings { get; set; }

    public async Task<IActionResult> OnGet()
    {
        Order = await _orderService.GetByUserId(User.GetUserId());
        if (Order == null)
            return RedirectToPage("../Index");
        UserAddresses = await _userAddressService.GetAll(User.GetUserId());
        Shippings = await _shippingService.GetAll();

        if (!Shippings.Any())
            return RedirectToPage("../Index");

        return Page();
    }

    public async Task<IActionResult> OnPost(long shippingMethodId)
    {
        var result = await _orderService.Checkout(shippingMethodId);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return RedirectToPage("Index").WithModelStateOf(this);
        }

        var order = await _orderService.GetByUserId(User.GetUserId());
        var transaction = await _transactionService.CreateTransaction(new CreateTransactionViewModel
        {
            OrderId = order.Id,
            ErrorCallbackUrl =
                $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/checkout/orderFinalized/{order.Id}",
            SuccessCallbackUrl =
                $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/checkout/orderFinalized/{order.Id}"
        });

        if (transaction.IsSuccessful)
            return Redirect(transaction.Data);

        return RedirectToPage("Index").WithModelStateOf(this);
    }
}