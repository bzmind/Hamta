using System.Net;
using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.DecreaseItemCount;
using Shop.Application.Orders.IncreaseOrderItemCount;
using Shop.Application.Orders.RemoveItem;
using Shop.Application.Orders.SetStatus;
using Shop.Presentation.Facade.Orders;
using Shop.Query.Orders._DTOs;

namespace Shop.API.Controllers;

public class OrderController : BaseApiController
{
    private readonly IOrderFacade _orderFacade;

    public OrderController(IOrderFacade orderFacade)
    {
        _orderFacade = orderFacade;
    }

    [HttpPost]
    public async Task<ApiResult<long>> AddItem(AddOrderItemCommand command)
    {
        var result = await _orderFacade.AddItem(command);
        var resultUrl = Url.Action("AddItem", "Order", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpDelete]
    public async Task<ApiResult> RemoveItem(RemoveOrderItemCommand command)
    {
        var result = await _orderFacade.RemoveItem(command);
        return CommandResult(result);
    }

    [HttpPut]
    public async Task<ApiResult> Checkout(CheckoutOrderCommand command)
    {
        var result = await _orderFacade.Checkout(command);
        return CommandResult(result);
    }

    [HttpPut("IncreaseItemCount")]
    public async Task<ApiResult> IncreaseItemCount(IncreaseOrderItemCountCommand command)
    {
        var result = await _orderFacade.IncreaseItemCount(command);
        return CommandResult(result);
    }

    [HttpPut("DecreaseItemCount")]
    public async Task<ApiResult> DecreaseItemCount(DecreaseOrderItemCountCommand command)
    {
        var result = await _orderFacade.DecreaseItemCount(command);
        return CommandResult(result);
    }

    [HttpPut("SetStatus")]
    public async Task<ApiResult> SetStatus(SetOrderStatusCommand command)
    {
        var result = await _orderFacade.SetStatus(command);
        return CommandResult(result);
    }

    [HttpGet("{orderId}")]
    public async Task<ApiResult<OrderDto?>> GetById(long orderId)
    {
        var result = await _orderFacade.GetById(orderId);
        return QueryResult(result);
    }

    [HttpGet]
    public async Task<ApiResult<OrderFilterResult>> GetByFilter([FromQuery] OrderFilterParam filterParams)
    {
        var result = await _orderFacade.GetByFilter(filterParams);
        return QueryResult(result);
    }
}