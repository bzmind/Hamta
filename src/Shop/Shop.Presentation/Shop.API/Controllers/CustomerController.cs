using System.Net;
using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Customers.AddFavoriteItem;
using Shop.Application.Customers.Create;
using Shop.Application.Customers.Edit;
using Shop.Application.Customers.Remove;
using Shop.Application.Customers.RemoveFavoriteItem;
using Shop.Application.Customers.SetAvatar;
using Shop.Application.Customers.SetSubscriptionToNews;
using Shop.Presentation.Facade.Customers;
using Shop.Query.Customers._DTOs;

namespace Shop.API.Controllers;

public class CustomerController : BaseApiController
{
    private readonly ICustomerFacade _customerFacade;

    public CustomerController(ICustomerFacade customerFacade)
    {
        _customerFacade = customerFacade;
    }

    [HttpPost]
    public async Task<ApiResult<long>> Create(CreateCustomerCommand command)
    {
        var result = await _customerFacade.Create(command);
        var resultUrl = Url.Action("Create", "Customer", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut]
    public async Task<ApiResult> Edit(EditCustomerCommand command)
    {
        var result = await _customerFacade.Edit(command);
        return CommandResult(result);
    }

    [HttpPut("SetAvatar")]
    public async Task<ApiResult> SetAvatar([FromForm] SetCustomerAvatarCommand command)
    {
        var result = await _customerFacade.SetAvatar(command);
        return CommandResult(result);
    }

    [HttpPut("SetSubscriptionToNews")]
    public async Task<ApiResult> SetSubscriptionToNews(SetCustomerSubscriptionToNewsCommand command)
    {
        var result = await _customerFacade.SetSubscriptionToNews(command);
        return CommandResult(result);
    }

    [HttpPut("AddFavoriteItem")]
    public async Task<ApiResult> AddFavoriteItem(AddCustomerFavoriteItemCommand command)
    {
        var result = await _customerFacade.AddFavoriteItem(command);
        return CommandResult(result);
    }

    [HttpDelete("RemoveFavoriteItem")]
    public async Task<ApiResult> RemoveFavoriteItem(RemoveCustomerFavoriteItemCommand command)
    {
        var result = await _customerFacade.RemoveFavoriteItem(command);
        return CommandResult(result);
    }

    [HttpDelete("{customerId}")]
    public async Task<ApiResult> Remove(long customerId)
    {
        var result = await _customerFacade.Remove(customerId);
        return CommandResult(result);
    }

    [HttpGet("{customerId}")]
    public async Task<ApiResult<CustomerDto?>> GetById(long customerId)
    {
        var result = await _customerFacade.GetById(customerId);
        return QueryResult(result);
    }

    [HttpGet("GetByPhoneNumber/{phoneNumber}")]
    public async Task<ApiResult<CustomerDto?>> GetByPhoneNumber(string phoneNumber)
    {
        var result = await _customerFacade.GetByPhoneNumber(phoneNumber);
        return QueryResult(result);
    }

    [HttpGet]
    public async Task<ApiResult<CustomerFilterResult>> GetByFilter([FromQuery] CustomerFilterParam filterParam)
    {
        var result = await _customerFacade.GetByFilter(filterParam);
        return QueryResult(result);
    }
}