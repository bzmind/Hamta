using System.Net;
using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveGalleryImage;
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
    public async Task<ApiResult<long>> Create([FromForm] CreateProductCommand command)
    {
        var result = await _productFacade.Create(command);
        var resultUrl = Url.Action("Create", "Product", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut]
    public async Task<ApiResult> Edit(EditProductCommand command)
    {
        var result = await _productFacade.Edit(command);
        return CommandResult(result);
    }

    [HttpDelete]
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

    [HttpGet("{id}")]
    public async Task<ApiResult<ProductDto?>> GetProductById(long id)
    {
        var result = await _productFacade.GetProductById(id);
        return QueryResult(result);
    }

    [HttpGet]
    public async Task<ApiResult<ProductFilterResult>> GetProductByFilter(ProductFilterParam filterParam)
    {
        var result = await _productFacade.GetProductByFilter(filterParam);
        return QueryResult(result);
    }
}