using System.Net;
using AutoMapper;
using Common.Api;
using Common.Api.Attributes;
using Common.Api.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Orders;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.DecreaseItemCount;
using Shop.Application.Orders.IncreaseOrderItemCount;
using Shop.Application.Orders.RemoveItem;
using Shop.Application.Orders.SetStatus;
using Shop.Domain.RoleAggregate;
using Shop.Presentation.Facade.Orders;
using Shop.Query.Orders._DTOs;

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
    public async Task<ApiResult<long>> AddItem(AddOrderItemCommandViewModel viewModel)
    {
        var command = _mapper.Map<AddOrderItemCommand>(viewModel);
        command.UserId = User.GetUserId();
        var result = await _orderFacade.AddItem(command);
        var resultUrl = Url.Action("AddItem", "Order", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut("Checkout")]
    public async Task<ApiResult> Checkout(CheckoutOrderCommandViewModel viewModel)
    {
        var command = _mapper.Map<CheckoutOrderCommand>(viewModel);
        command.UserId = User.GetUserId();
        var result = await _orderFacade.Checkout(command);
        return CommandResult(result);
    }

    [HttpPut("IncreaseItemCount")]
    public async Task<ApiResult> IncreaseItemCount(IncreaseOrderItemCountCommandViewModel viewModel)
    {
        var command = _mapper.Map<IncreaseOrderItemCountCommand>(viewModel);
        command.UserId = User.GetUserId();
        var result = await _orderFacade.IncreaseItemCount(command);
        return CommandResult(result);
    }

    [HttpPut("DecreaseItemCount/{orderItemId}")]
    public async Task<ApiResult> DecreaseItemCount(long orderItemId)
    {
        var command = new DecreaseOrderItemCountCommand(User.GetUserId(), orderItemId);
        var result = await _orderFacade.DecreaseItemCount(command);
        return CommandResult(result);
    }

    [CheckPermission(RolePermission.Permissions.OrderManager)]
    [HttpPut("SetStatus")]
    public async Task<ApiResult> SetStatus(SetOrderStatusCommand command)
    {
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

    [CheckPermission(RolePermission.Permissions.OrderManager)]
    [HttpGet("GetByFilter")]
    public async Task<ApiResult<OrderFilterResult>> GetByFilter([FromQuery] OrderFilterParam filterParams)
    {
        var result = await _orderFacade.GetByFilter(filterParams);
        return QueryResult(result);
    }
}