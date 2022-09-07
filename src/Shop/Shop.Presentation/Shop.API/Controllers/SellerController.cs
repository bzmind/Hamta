using Common.Api;
using Common.Api.Attributes;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.RoleAggregate;
using System.Net;
using AutoMapper;
using Common.Api.Utility;
using Shop.API.ViewModels.Sellers;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Presentation.Facade.Sellers;
using Shop.Query.Sellers._DTOs;
using Shop.API.ViewModels.Sellers.Inventories;
using Shop.Application.Sellers.Inventories.Add;
using Shop.Application.Sellers.Inventories.DecreaseQuantity;
using Shop.Application.Sellers.Inventories.Edit;
using Shop.Application.Sellers.Inventories.IncreaseQuantity;
using Shop.Application.Sellers.Inventories.Remove;

namespace Shop.API.Controllers;

[CheckPermission(RolePermission.Permissions.SellerManager)]
public class SellerController : BaseApiController
{
    private readonly ISellerFacade _sellerFacade;
    private readonly IMapper _mapper;

    public SellerController(ISellerFacade sellerFacade, IMapper mapper)
    {
        _sellerFacade = sellerFacade;
        _mapper = mapper;
    }

    [HttpPost("Create")]
    public async Task<ApiResult<long>> Create(CreateSellerViewModel model)
    {
        var command = _mapper.Map<CreateSellerCommand>(model);
        command.UserId = User.GetUserId();
        var result = await _sellerFacade.Create(command);
        var resultUrl = Url.Action("Create", "Seller", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut("Edit")]
    public async Task<ApiResult> Edit(EditSellerViewModel model)
    {
        var command = _mapper.Map<EditSellerCommand>(model);
        command.UserId = User.GetUserId();
        var result = await _sellerFacade.Edit(command);
        return CommandResult(result);
    }

    [HttpDelete("Remove")]
    public async Task<ApiResult> Remove()
    {
        var result = await _sellerFacade.Remove(User.GetUserId());
        return CommandResult(result);
    }

    [HttpGet("GetCurrentSeller")]
    public async Task<ApiResult<SellerDto?>> GetCurrentSeller()
    {
        var result = await _sellerFacade.GetCurrentSeller(User.GetUserId());
        return QueryResult(result);
    }

    [HttpGet("GetByFilter")]
    public async Task<ApiResult<SellerFilterResult>> GetByFilter([FromQuery] SellerFilterParams filterParams)
    {
        var result = await _sellerFacade.GetByFilter(filterParams);
        return QueryResult(result);
    }

    [HttpPost("AddInventory")]
    public async Task<ApiResult<long>> AddInventory(AddSellerInventoryViewModel model)
    {
        var command = _mapper.Map<AddSellerInventoryCommand>(model);
        command.UserId = User.GetUserId();
        var result = await _sellerFacade.AddInventory(command);
        var resultUrl = Url.Action("AddInventory", "Seller", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut("EditInventory")]
    public async Task<ApiResult> EditInventory(EditSellerInventoryViewModel model)
    {
        var command = _mapper.Map<EditSellerInventoryCommand>(model);
        command.UserId = User.GetUserId();
        var result = await _sellerFacade.EditInventory(command);
        return CommandResult(result);
    }

    [HttpPut("IncreaseInventoryQuantity")]
    public async Task<ApiResult> IncreaseInventoryQuantity(IncreaseSellerInventoryQuantityViewModel model)
    {
        var command = _mapper.Map<IncreaseSellerInventoryQuantityCommand>(model);
        command.UserId = User.GetUserId();
        var result = await _sellerFacade.IncreaseInventoryQuantity(command);
        return CommandResult(result);
    }

    [HttpPut("DecreaseInventoryQuantity")]
    public async Task<ApiResult> DecreaseInventoryQuantity(DecreaseSellerInventoryQuantityViewModel model)
    {
        var command = _mapper.Map<DecreaseSellerInventoryQuantityCommand>(model);
        command.UserId = User.GetUserId();
        var result = await _sellerFacade.DecreaseInventoryQuantity(command);
        return CommandResult(result);
    }

    [HttpDelete("RemoveInventory/{inventoryId}")]
    public async Task<ApiResult> RemoveInventory(long inventoryId)
    {
        var command = new RemoveSellerInventoryCommand(User.GetUserId(), inventoryId);
        var result = await _sellerFacade.RemoveInventory(command);
        return CommandResult(result);
    }

    [HttpGet("GetInventoryById/{inventoryId}")]
    public async Task<ApiResult<SellerInventoryDto?>> GetInventoryById(long inventoryId)
    {
        var result = await _sellerFacade.GetInventoryById(inventoryId);
        return QueryResult(result);
    }

    [HttpGet("GetInventoriesByFilter")]
    public async Task<ApiResult<SellerInventoryFilterResult>> GetByFilter
        ([FromQuery] SellerInventoryFilterParams filterParams)
    {
        var result = await _sellerFacade.GetInventoriesByFilter(filterParams);
        return QueryResult(result);
    }
}