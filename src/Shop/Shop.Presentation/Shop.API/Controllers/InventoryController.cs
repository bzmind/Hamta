using System.Net;
using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Inventories.Create;
using Shop.Application.Inventories.DiscountByPercentage;
using Shop.Application.Inventories.Edit;
using Shop.Application.Inventories.RemoveDiscount;
using Shop.Presentation.Facade.Inventories;
using Shop.Query.Inventories._DTOs;

namespace Shop.API.Controllers;

public class InventoryController : BaseApiController
{
    private readonly IInventoryFacade _inventoryFacade;

    public InventoryController(IInventoryFacade inventoryFacade)
    {
        _inventoryFacade = inventoryFacade;
    }

    [HttpPost]
    public async Task<ApiResult<long>> Create(CreateInventoryCommand command)
    {
        var result = await _inventoryFacade.Create(command);
        var resultUrl = Url.Action("Create", "Inventory", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut]
    public async Task<ApiResult> Edit(EditInventoryCommand command)
    {
        var result = await _inventoryFacade.Edit(command);
        return CommandResult(result);
    }

    [HttpPut("DiscountByPercentage")]
    public async Task<ApiResult> DiscountByPercentage(DiscountByPercentageCommand command)
    {
        var result = await _inventoryFacade.DiscountByPercentage(command);
        return CommandResult(result);
    }

    [HttpPut("RemoveDiscount")]
    public async Task<ApiResult> RemoveDiscount(RemoveInventoryDiscountCommand command)
    {
        var result = await _inventoryFacade.RemoveDiscount(command);
        return CommandResult(result);
    }

    [HttpDelete]
    public async Task<ApiResult> Remove(long inventoryId)
    {
        var result = await _inventoryFacade.Remove(inventoryId);
        return CommandResult(result);
    }

    [HttpGet("{inventoryId}")]
    public async Task<ApiResult<InventoryDto?>> GetById(long inventoryId)
    {
        var result = await _inventoryFacade.GetById(inventoryId);
        return QueryResult(result);
    }

    [HttpGet]
    public async Task<ApiResult<InventoryFilterResult>> GetByFilter
        ([FromQuery] InventoryFilterParam filterParams)
    {
        var result = await _inventoryFacade.GetByFilter(filterParams);
        return QueryResult(result);
    }
}