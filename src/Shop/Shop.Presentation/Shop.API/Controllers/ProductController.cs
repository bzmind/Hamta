using System.Net;
using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Shop.API.CustomModelBinders;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.Remove;
using Shop.Application.Products.RemoveGalleryImage;
using Shop.Application.Products.ReplaceMainImage;
using Shop.Application.Products.SetScore;
using Shop.Presentation.Facade.Products;
using Shop.Query.Products._DTOs;

namespace Shop.API.Controllers;

public class ProductController : BaseApiController
{
    private readonly IProductFacade _productFacade;

    public ProductController(IProductFacade productFacade)
    {
        _productFacade = productFacade;
    }

    [HttpPost]
    public async Task<ApiResult<long>> Create
        ([FromForm][ModelBinder(typeof(ProductModelBinder))] CreateProductCommand command)
    {
        var result = await _productFacade.Create(command);
        var resultUrl = Url.Action("Create", "Product", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut]
    public async Task<ApiResult> Edit
        ([FromForm][ModelBinder(typeof(ProductModelBinder))] EditProductCommand command)
    {
        var result = await _productFacade.Edit(command);
        return CommandResult(result);
    }

    [HttpPut("ReplaceMainImage")]
    public async Task<ApiResult> ReplaceMainImage([FromForm] ReplaceMainImageCommand command)
    {
        var result = await _productFacade.ReplaceMainImage(command);
        return CommandResult(result);
    }

    [HttpDelete("RemoveGalleryImage")]
    public async Task<ApiResult> RemoveGalleryImages(RemoveGalleryImageCommand command)
    {
        var result = await _productFacade.RemoveGalleryImage(command);
        return CommandResult(result);
    }

    [HttpPut("AddScore")]
    public async Task<ApiResult> AddScore(AddScoreCommand command)
    {
        var result = await _productFacade.AddScore(command);
        return CommandResult(result);
    }

    [HttpDelete("{productId}")]
    public async Task<ApiResult> Remove(long productId)
    {
        var result = await _productFacade.Remove(productId);
        return CommandResult(result);
    }
    
    [HttpGet("{id}")]
    public async Task<ApiResult<ProductDto?>> GetById(long id)
    {
        var result = await _productFacade.GetById(id);
        return QueryResult(result);
    }

    [HttpGet]
    public async Task<ApiResult<ProductFilterResult>> GetByFilter([FromQuery] ProductFilterParam filterParam)
    {
        var result = await _productFacade.GetByFilter(filterParam);
        return QueryResult(result);
    }
}