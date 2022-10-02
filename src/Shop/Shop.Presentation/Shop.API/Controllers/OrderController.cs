using Common.Api;
using Common.Api.Attributes;
using Common.Api.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.RemoveItem;
using Shop.Application.Orders.SetStatus;
using Shop.Domain.RoleAggregate;
using Shop.Presentation.Facade.Orders;
using Shop.Query.Orders._DTOs;
using System.Net;
using AutoMapper;
using Shop.API.ViewModels.Orders;

namespace Shop.API.Controllers;

[Authorize]
public class OrderController : BaseApiController
{
    private readonly IOrderFacade _orderFacade;
    private readonly IMapper _mapper;

    public OrderController(IOrderFacade orderFacade, IMapper mapper)
    {
        _orderFacade = orderFacade;
        _mapper = mapper;
    }

    [HttpPost("AddItem")]
    public async Task<ApiResult<long>> AddItem(AddOrderItemViewModel model)
    {
        var command = _mapper.Map<AddOrderItemCommand>(model);
        command.UserId = User.GetUserId();
        var result = await _orderFacade.AddItem(command);
        var resultUrl = Url.Action("AddItem", "Order", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut("Checkout")]
    public async Task<ApiResult> Checkout(CheckoutOrderViewModel model)
    {
        var command = _mapper.Map<CheckoutOrderCommand>(model);
        command.UserId = User.GetUserId();
        var result = await _orderFacade.Checkout(command);
        return CommandResult(result);
    }

    [HttpPut("IncreaseItemCount/{orderItemId}")]
    public async Task<ApiResult> IncreaseItemCount(long orderItemId)
    {
        var result = await _orderFacade.IncreaseItemCount(User.GetUserId(), orderItemId);
        return CommandResult(result);
    }

    [HttpPut("DecreaseItemCount/{orderItemId}")]
    public async Task<ApiResult> DecreaseItemCount(long orderItemId)
    {
        var result = await _orderFacade.DecreaseItemCount(User.GetUserId(), orderItemId);
        return CommandResult(result);
    }

    [CheckPermission(RolePermission.Permissions.OrderManager)]
    [HttpPut("SetStatus")]
    public async Task<ApiResult> SetStatus(SetOrderStatusViewModel model)
    {
        var command = _mapper.Map<SetOrderStatusCommand>(model);
        var result = await _orderFacade.SetStatus(command);
        return CommandResult(result);
    }

    [HttpDelete("RemoveItem/{orderItemId}")]
    public async Task<ApiResult> RemoveItem(long orderItemId)
    {
        var command = new RemoveOrderItemCommand(User.GetUserId(), orderItemId);
        var result = await _orderFacade.RemoveItem(command);
        return CommandResult(result);
    }

    [CheckPermission(RolePermission.Permissions.OrderManager)]
    [HttpGet("GetById/{orderId}")]
    public async Task<ApiResult<OrderDto?>> GetById(long orderId)
    {
        var result = await _orderFacade.GetById(orderId);
        return QueryResult(result);
    }

    [HttpGet("GetByUserId/{userId}")]
    public async Task<ApiResult<OrderDto?>> GetByUserId(long userId)
    {
        var result = await _orderFacade.GetByUserId(userId);
        return QueryResult(result);
    }

    [CheckPermission(RolePermission.Permissions.OrderManager)]
    [HttpGet("GetByFilter")]
    public async Task<ApiResult<OrderFilterResult>> GetByFilter([FromQuery] OrderFilterParams filterParams)
    {
        var result = await _orderFacade.GetByFilter(filterParams);
        return QueryResult(result);
    }
}