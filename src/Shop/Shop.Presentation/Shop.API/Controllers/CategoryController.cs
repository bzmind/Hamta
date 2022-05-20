using System.Net;
using System.Text.Json;
using Common.Api;
using Common.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Shop.Application.Categories.AddSubCategory;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Presentation.Facade.Categories;
using Shop.Query.Categories._DTOs;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Shop.API.Controllers;

[ModelBinder(typeof(CreateCategoryModelBinder))]
public record CreateCategoryModel(string Title, string Slug, List<Specification>? Specifications);

public class CreateCategoryModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var title = "";
        var slug = "";

        bindingContext.ModelMetadata.Properties.ToList().ForEach(property =>
        {
            var propertyName = property.Name;

            switch (propertyName)
            {
                case "Title":
                    title = bindingContext.ValueProvider.GetValue(propertyName).FirstValue;
                    break;
                case "Slug":
                    slug = bindingContext.ValueProvider.GetValue(propertyName).FirstValue;
                    break;
            }
        });

        var specifications = bindingContext.ValueProvider.GetValue("Specifications");

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var specs = new List<Specification>();

        List<Specification> specList = JsonConvert.DeserializeObject<List<Specification>>(specifications.ToString());

        specifications.Values.ToList().ForEach(spec =>
        {
            var deserialized = JsonSerializer.Deserialize(spec, typeof(Specification), options);
            specs.Add((Specification)deserialized);
        });
        
        //var listLength = bindingContext.ValueProvider.GetValue(providerResultKeys.First().Value).Length;


        //for (int i = 0; i < listLength; i++)
        //{
        //    var spec = new Specification();

        //    for (int j = 0; j < providerResultKeys.Count; j++)
        //    {
        //        var key = providerResultKeys.ElementAt(j).Value;
        //        var value = bindingContext.ValueProvider.GetValue(key).ElementAt(i);

        //        if (key.ToLower() == $"{specifications}[{nameof(Specification.Title)}]".ToLower())
        //            spec.Title = value;

        //        if (key.ToLower() == $"{specifications}[{nameof(Specification.Description)}]".ToLower())
        //            spec.Description = value;

        //        if (key.ToLower() == $"{specifications}[{nameof(Specification.IsImportantFeature)}]".ToLower())
        //            spec.IsImportantFeature = bool.Parse(value);

        //        //bindingContext.ModelState.SetModelValue(key, bindingContext.ValueProvider.GetValue(key));
        //    }

        //}

        var model = new CreateCategoryModel(title, slug, specs);
        bindingContext.Result = ModelBindingResult.Success(model);
        return Task.CompletedTask;
    }
}

public class CategoryController : BaseApiController
{
    private readonly ICategoryFacade _categoryFacade;

    public CategoryController(ICategoryFacade categoryFacade)
    {
        _categoryFacade = categoryFacade;
    }

    [HttpPost]
    public async Task<ApiResult<long>> CreateCategory([FromForm] CreateCategoryModel command)
    {
        var model = new CreateCategoryCommand(command.Title, command.Slug, command.Specifications);
        var result = await _categoryFacade.Create(model);
        //var result = OperationResult<long>.Success(1);
        var resultUrl = Url.Action("CreateCategory", "Category", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut]
    public async Task<ApiResult> EditCategory(EditCategoryCommand command)
    {
        var result = await _categoryFacade.Edit(command);
        return CommandResult(result);
    }

    [HttpPost("AddSubCategory")]
    public async Task<ApiResult<long>> AddSubCategory(AddSubCategoryCommand command)
    {
        var result = await _categoryFacade.AddSubCategory(command);
        var resultUrl = Url.Action("AddSubCategory", "Category", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpDelete("{subCategoryId}")]
    public async Task<ApiResult> RemoveCategory(long subCategoryId)
    {
        var result = await _categoryFacade.RemoveCategory(subCategoryId);
        return CommandResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ApiResult<CategoryDto?>> GetCategoryById(long id)
    {
        var result = await _categoryFacade.GetCategoryById(id);
        return QueryResult(result);
    }

    [HttpGet("getSubCategories/{parentId}")]
    public async Task<ApiResult<List<CategoryDto>>> GetCategoryByParentId(long parentId)
    {
        var result = await _categoryFacade.GetCategoryByParentId(parentId);
        return QueryResult(result);
    }

    [HttpGet]
    public async Task<ApiResult<List<CategoryDto>>> GetCategories()
    {
        var result = await _categoryFacade.GetCategoryList();
        return QueryResult(result);
    }
}