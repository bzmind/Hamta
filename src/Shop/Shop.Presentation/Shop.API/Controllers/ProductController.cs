using System.Net;
using System.Text.Json;
using Common.Api;
using Common.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveGalleryImage;
using Shop.Application.Products.SetScore;
using Shop.Presentation.Facade.Products;
using Shop.Query.Products._DTOs;

namespace Shop.API.Controllers;

[ModelBinder(typeof(CreateProductModelBinder))]
public class CreateProductModel
{
    public long CategoryId { get; set; }
    public string Name { get; set; }
    public string? EnglishName { get; set; }
    public string Slug { get; set; }
    public string? Description { get; set; }
    public IFormFile MainImage { get; set; }
    public List<IFormFile> GalleryImages { get; set; }
    public List<Specification>? CustomSpecifications { get; set; } = new();
    public Dictionary<string, string>? ExtraDescriptions { get; set; } = new();
}

public class CreateProductModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var model = new CreateProductModel();

        bindingContext.ModelMetadata.Properties.ToList().ForEach(property =>
        {
            var propertyName = property.Name;

            if (string.IsNullOrEmpty(propertyName))
                return;

            var propertyValue = bindingContext.ValueProvider.GetValue(propertyName).FirstValue;

            if (propertyValue == null || string.IsNullOrEmpty(propertyValue))
                return;

            switch (propertyName)
            {
                case nameof(model.CategoryId):
                    model.CategoryId = long.Parse(propertyValue);
                    break;
                case nameof(model.Name):
                    model.Name = propertyValue;
                    break;
                case nameof(model.EnglishName):
                    model.EnglishName = propertyValue;
                    break;
                case nameof(model.Slug):
                    model.Slug = propertyValue;
                    break;
                case nameof(model.Description):
                    model.Description = propertyValue;
                    break;
            }
        });

        var formFiles = bindingContext.ActionContext.HttpContext.Request.Form.Files;
        model.MainImage = formFiles.GetFile(nameof(model.MainImage));
        model.GalleryImages = formFiles.GetFiles(nameof(model.GalleryImages)).ToList();

        var customSpecifications = bindingContext.ValueProvider.GetValue(nameof(model.CustomSpecifications));
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        customSpecifications.Values.ToList().ForEach(spec =>
        {
            var deserializedSpec = JsonSerializer.Deserialize(spec, typeof(Specification), options);
            model.CustomSpecifications.Add((Specification)deserializedSpec);
        });

        var extraDescriptions = bindingContext.ValueProvider.GetValue(nameof(model.ExtraDescriptions))
            .FirstValue;
        var deserializedDesc =
            JsonSerializer.Deserialize(extraDescriptions, typeof(Dictionary<string, string>), options);
        model.ExtraDescriptions = (Dictionary<string, string>)deserializedDesc;

        bindingContext.Result = ModelBindingResult.Success(model);
        return Task.CompletedTask;
    }
}

public class ProductController : BaseApiController
{
    private readonly IProductFacade _productFacade;

    public ProductController(IProductFacade productFacade)
    {
        _productFacade = productFacade;
    }

    [HttpPost]
    public async Task<ApiResult<long>> Create([FromForm] CreateProductModel model)
    {
        var command = new CreateProductCommand(model.CategoryId, model.Name, model.EnglishName, model.Slug,
            model.Description, model.MainImage, model.GalleryImages, model.CustomSpecifications,
            model.ExtraDescriptions);
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