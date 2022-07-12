using Common.Api;
using Common.Api.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.API.CustomModelBinders;
using Shop.Application.Products.AddScore;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveGalleryImage;
using Shop.Application.Products.ReplaceMainImage;
using Shop.Domain.RoleAggregate;
using Shop.Presentation.Facade.Products;
using Shop.Query.Products._DTOs;
using System.Net;
using AutoMapper;
using Shop.API.ViewModels.Products;
using Shop.Application.Products.Create;

namespace Shop.API.Controllers;

[CheckPermission(RolePermission.Permissions.ProductManager)]
public class ProductController : BaseApiController
{
    private readonly IProductFacade _productFacade;
    private readonly IMapper _mapper;

    public ProductController(IProductFacade productFacade, IMapper mapper)
    {
        _productFacade = productFacade;
        _mapper = mapper;
    }

    [HttpPost("Create")]
    public async Task<ApiResult<long>> Create
        ([FromForm][ModelBinder(typeof(ProductModelBinder))] CreateProductViewModel model)
    {
        var command = _mapper.Map<CreateProductCommand>(model);
        var result = await _productFacade.Create(command);
        var resultUrl = Url.Action("Create", "Product", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut("Edit")]
    public async Task<ApiResult> Edit
        ([FromForm][ModelBinder(typeof(ProductModelBinder))] EditProductViewModel model)
    {
        var command = _mapper.Map<EditProductCommand>(model);
        var result = await _productFacade.Edit(command);
        return CommandResult(result);
    }

    [HttpPut("ReplaceMainImage")]
    public async Task<ApiResult> ReplaceMainImage([FromForm] ReplaceProductMainImageViewModel model)
    {
        var command = _mapper.Map<ReplaceProductMainImageCommand>(model);
        var result = await _productFacade.ReplaceMainImage(command);
        return CommandResult(result);
    }

    [HttpPut("AddScore")]
    public async Task<ApiResult> AddScore(AddProductScoreViewModel model)
    {
        var command = _mapper.Map<AddProductScoreCommand>(model);
        var result = await _productFacade.AddScore(command);
        return CommandResult(result);
    }

    [HttpPut("RemoveGalleryImage")]
    public async Task<ApiResult> RemoveGalleryImages(RemoveProductGalleryImageViewModel model)
    {
        var command = _mapper.Map<RemoveProductGalleryImageCommand>(model);
        var result = await _productFacade.RemoveGalleryImage(command);
        return CommandResult(result);
    }

    [HttpDelete("Remove/{productId}")]
    public async Task<ApiResult> Remove(long productId)
    {
        var result = await _productFacade.Remove(productId);
        return CommandResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetById/{id}")]
    public async Task<ApiResult<ProductDto?>> GetById(long id)
    {
        var result = await _productFacade.GetById(id);
        return QueryResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetByFilter")]
    public async Task<ApiResult<ProductFilterResult>> GetByFilter([FromQuery] ProductFilterParams filterParams)
    {
        var result = await _productFacade.GetByFilter(filterParams);
        return QueryResult(result);
    }
}