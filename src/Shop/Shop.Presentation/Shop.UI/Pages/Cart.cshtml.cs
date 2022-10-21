using Common.Api.Utility;
using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Orders;
using Shop.Query.Orders._DTOs;
using Shop.UI.Services.Orders;
using Shop.UI.Setup.CookieUtility;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages;

public class CartModel : BaseRazorPage
{
    private readonly IOrderService _orderService;
    private readonly CartCookieManager _cartCookieManager;

    public CartModel(IRazorToStringRenderer razorToStringRenderer, IOrderService orderService,
        CartCookieManager cartCookieManager) : base(razorToStringRenderer)
    {
        _orderService = orderService;
        _cartCookieManager = cartCookieManager;
    }

    public OrderDto? Order { get; set; }

    public async Task OnGet()
    {
        if (User.Identity.IsAuthenticated)
        {
            Order = await _orderService.GetByUserId(User.GetUserId());
        }
        else
        {
            Order = _cartCookieManager.GetCart();
        }
    }

    public async Task<IActionResult> OnPostAddItem(long inventoryId, int quantity)
    {
        if (User.Identity.IsAuthenticated)
        {
            var result = await _orderService.AddItem(new AddOrderItemViewModel
            {
                UserId = User.GetUserId(),
                InventoryId = inventoryId,
                Quantity = quantity
            });
            if (!result.IsSuccessful)
                MakeAlert(result);

            return AjaxRedirectToPageResult("Cart");
        }
        else
        {
            var result = await _cartCookieManager.AddItem(inventoryId, quantity);
            if (!result.IsSuccessful)
                MakeAlert(result);

            return AjaxRedirectToPageResult("Cart");
        }
    }

    public async Task<IActionResult> OnPostIncreaseItemCount(long inventoryId, long itemId)
    {
        if (User.Identity.IsAuthenticated)
        {
            var result = await _orderService.IncreaseItemCount(itemId);
            if (!result.IsSuccessful)
            {
                MakeAlert(result);
                return AjaxErrorMessageResult(result);
            }
            return AjaxReloadCurrentPageResult();
        }
        else
        {
            var result = await _cartCookieManager.Increase(inventoryId, itemId);
            if (!result.IsSuccessful)
            {
                MakeAlert(result);
                return AjaxErrorMessageResult(result);
            }
            return AjaxReloadCurrentPageResult();
        }
    }

    public async Task<IActionResult> OnPostDecreaseItemCount(long itemId)
    {
        if (User.Identity.IsAuthenticated)
        {
            var result = await _orderService.DecreaseItemCount(itemId);
            if (!result.IsSuccessful)
            {
                MakeAlert(result);
                return AjaxErrorMessageResult(result);
            }
            return AjaxReloadCurrentPageResult();
        }
        else
        {
            var result = _cartCookieManager.Decrease(itemId);
            if (!result.IsSuccessful)
            {
                MakeAlert(result);
                return AjaxErrorMessageResult(result);
            }
            return AjaxReloadCurrentPageResult();
        }
    }

    public async Task<IActionResult> OnPostRemoveItem(long itemId)
    {
        if (User.Identity.IsAuthenticated)
        {
            var result = await _orderService.RemoveItem(itemId);
            if (!result.IsSuccessful)
                MakeAlert(result);

            return AjaxRedirectToPageResult("Cart");
        }
        else
        {
            var result = _cartCookieManager.RemoveItem(itemId);
            if (!result.IsSuccessful)
                MakeAlert(result);

            return AjaxRedirectToPageResult("Cart");
        }
    }

    public async Task<IActionResult> OnGetCartSummary()
    {
        var orderDto = new OrderDto();
        if (User.Identity.IsAuthenticated)
        {
            orderDto = await _orderService.GetByUserId(User.GetUserId());
        }
        else
        {
            orderDto = _cartCookieManager.GetCart();
        }

        return new ObjectResult(new
        {
            items = orderDto?.Items,
            itemsCount = orderDto?.Items.Sum(i => i.Count),
            price = $"{orderDto?.Items.Sum(i => i.EachItemDiscountedPrice * i.Count):#,0} تومان"
        });
    }
}