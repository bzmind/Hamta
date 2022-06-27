﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Shop.UI.SetupClasses.RazorUtility;

public interface IRazorToStringRenderer
{
    Task<string> RenderToStringAsync(string viewName, object model, PageContext context);
}

public class RazorToStringRenderer : IRazorToStringRenderer
{
    private readonly IRazorViewEngine _razorViewEngine;
    private readonly ITempDataProvider _tempDataProvider;

    public RazorToStringRenderer(IRazorViewEngine razorViewEngine, ITempDataProvider tempDataProvider)
    {
        _razorViewEngine = razorViewEngine;
        _tempDataProvider = tempDataProvider;
    }

    public async Task<string> RenderToStringAsync(string viewName, object model, PageContext context)
    {
        await using var sw = new StringWriter();
        var viewResult = _razorViewEngine.FindView(context, viewName, false);

        if (viewResult.View == null)
        {
            throw new ArgumentNullException($"{viewName} does not match any available view");
        }

        var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
        {
            Model = model
        };

        var viewContext = new ViewContext(
            context,
            viewResult.View,
            viewDictionary,
            new TempDataDictionary(context.HttpContext, _tempDataProvider),
            sw,
            new HtmlHelperOptions()
        );

        await viewResult.View.RenderAsync(viewContext);
        return sw.ToString();
    }
}