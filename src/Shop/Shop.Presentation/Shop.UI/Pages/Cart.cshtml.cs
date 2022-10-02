using Common.Api.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.API.ViewModels.Orders;
using Shop.Query.Orders._DTOs;
using Shop.Query.Orders._Mappers;
using Shop.UI.Services.Orders;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages;

public class CartModel : BaseRazorPage
{
    private readonly IOrderService _orderService;

    public CartModel(IRazorToStringRenderer razorToStringRenderer,
        IOrderService orderService) : base(razorToStringRenderer)
    {
        _orderService = orderService;
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
        }
    }

    public async Task<IActionResult> OnPostAddItem(long inventoryId, int quantity)
    {
        if (User.Identity.IsAuthenticated)
        {
            var result = await _orderService.AddItem(new AddOrderItemViewModel
            {
                InventoryId = inventoryId,
                Quantity = quantity
            });
            if (!result.IsSuccessful)
                MakeAlert(result);

            return AjaxRedirectToPageResult("Cart");
        }
        else
        {
            return Page();
        }
    }

    public async Task<IActionResult> OnPostIncreaseItemCount(long itemId)
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
            return Page();
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
            return Page();
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
            return Page();
        }
    }
}