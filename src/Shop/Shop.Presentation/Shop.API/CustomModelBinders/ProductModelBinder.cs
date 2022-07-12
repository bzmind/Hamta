using System.Reflection;
using System.Text.Json;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shop.API.SetupClasses;
using Shop.API.ViewModels;
using Shop.API.ViewModels.Products;
using Shop.Application;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;

namespace Shop.API.CustomModelBinders;

internal class TempProductModel
{
    public long ProductId { get; set; }
    public long CategoryId { get; set; }
    public string Name { get; set; }
    public string EnglishName { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public IFormFile MainImage { get; set; }
    public List<IFormFile> GalleryImages { get; set; } = new();
    public List<SpecificationViewModel> CustomSpecifications { get; set; } = new();
    public Dictionary<string, string> ExtraDescriptions { get; set; } = new();
}

public class ProductModelBinder : IModelBinder
{
    private IMapper _mapper;

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        _mapper = (IMapper)bindingContext.ActionContext.HttpContext.RequestServices.GetService(typeof(AutoMapperProfile));
        var tempModel = new TempProductModel();

        var type = tempModel.GetType();
        var tempModelProperties = new List<PropertyInfo>(type.GetProperties());

        bindingContext.ModelMetadata.Properties.ToList().ForEach(property =>
        {
            var propertyName = property.Name;

            if (string.IsNullOrEmpty(propertyName))
                return;

            var propertyValue = bindingContext.ValueProvider.GetValue(propertyName).FirstValue;

            if (propertyValue == null || string.IsNullOrEmpty(propertyValue))
                return;

            var p = tempModelProperties.FirstOrDefault(p => p.Name == propertyName);

            if (p != null)
            {
                try
                {
                    p.SetValue(tempModel, propertyValue);
                }
                catch (ArgumentException e)
                {
                    try
                    {
                        p.SetValue(tempModel, long.Parse(propertyValue));
                    }
                    catch (FormatException exception)
                    {
                    }
                }
            }
        });

        var formFiles = bindingContext.ActionContext.HttpContext.Request.Form.Files;
        tempModel.MainImage = formFiles.GetFile(nameof(tempModel.MainImage));
        tempModel.GalleryImages = formFiles.GetFiles(nameof(tempModel.GalleryImages)).ToList();

        var customSpecifications = bindingContext.ValueProvider.GetValue(nameof(tempModel.CustomSpecifications));
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        customSpecifications.Values.ToList().ForEach(spec =>
        {
            var deserializedSpec = JsonSerializer.Deserialize(spec, typeof(SpecificationViewModel), options);
            tempModel.CustomSpecifications.Add((SpecificationViewModel)deserializedSpec);
        });

        var extraDescriptions = bindingContext.ValueProvider
            .GetValue(nameof(tempModel.ExtraDescriptions)).FirstValue;
        if (extraDescriptions != null)
        {
            var deserializedDesc = JsonSerializer.Deserialize
                (extraDescriptions, typeof(Dictionary<string, string>), options);
            tempModel.ExtraDescriptions = (Dictionary<string, string>)deserializedDesc;
        }

        if (bindingContext.ModelType == typeof(CreateProductViewModel))
        {
            var specs = _mapper.Map<List<SpecificationDto>>(tempModel.CustomSpecifications);
            var model = new CreateProductCommand(tempModel.CategoryId, tempModel.Name, tempModel.EnglishName,
                tempModel.Slug, tempModel.Description, tempModel.MainImage, tempModel.GalleryImages,
                specs, tempModel.ExtraDescriptions);

            var validator = new CreateProductCommandValidator();
            var result = validator.Validate(model);
            result.AddToModelState(bindingContext.ModelState, null);
            bindingContext.Result = ModelBindingResult.Success(model);
        }
        else if (bindingContext.ModelType == typeof(EditProductCommand))
        {
            var specs = _mapper.Map<List<SpecificationDto>>(tempModel.CustomSpecifications);
            var model = new EditProductCommand(tempModel.ProductId, tempModel.CategoryId, tempModel.Name,
                tempModel.EnglishName, tempModel.Slug, tempModel.Description, tempModel.MainImage,
                tempModel.GalleryImages, specs, tempModel.ExtraDescriptions);

            var validator = new EditProductCommandValidator();
            var result = validator.Validate(model);
            result.AddToModelState(bindingContext.ModelState, null);
            bindingContext.Result = ModelBindingResult.Success(model);
        }

        return Task.CompletedTask;
    }
}