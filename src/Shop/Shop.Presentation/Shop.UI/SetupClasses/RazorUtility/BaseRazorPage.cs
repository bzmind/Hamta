﻿using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Shop.UI.SetupClasses.RazorUtility;

[ValidateAntiForgeryToken]
public class BaseRazorPage : PageModel
{
    private readonly IRazorToStringRenderer _razorToStringRenderer;
        
    public BaseRazorPage(IRazorToStringRenderer razorToStringRenderer)
    {
        _razorToStringRenderer = razorToStringRenderer;
    }

    protected void MakeAlert(ApiResult apiResult)
    {
        var model = JsonConvert.SerializeObject(apiResult);
        HttpContext.Response.Cookies.Append("alert", model);
    }

    protected void MakeAlert(string message)
    {
        var apiResult = new ApiResult { MetaData = new MetaData { Message = message } };
        var model = JsonConvert.SerializeObject(apiResult);
        HttpContext.Response.Cookies.Append("alert", model);
    }

    protected ContentResult AjaxHtmlResult<T>(ApiResult<T> apiResult)
    {
        var model = new AjaxResult
        {
            IsHtml = true,
            Message = apiResult.MetaData.Message,
            Data = apiResult.Data,
            StatusCode = apiResult.MetaData.ApiStatusCode
        };
        return Content(JsonConvert.SerializeObject(model));
    }

    protected ContentResult AjaxErrorMessageResult(ApiResult apiResult)
    {
        var model = new AjaxResult
        {
            Message = apiResult.MetaData.Message,
            StatusCode = apiResult.MetaData.ApiStatusCode
        };
        return Content(JsonConvert.SerializeObject(model));
    }

    protected ContentResult AjaxRedirectToPageResult(string page)
    {
        var path = Url.PageLink(page);
        if (string.IsNullOrWhiteSpace(path))
            path = $"..{page}";

        var model = new AjaxResult
        {
            IsRedirection = true,
            RedirectPath = path
        };
        return Content(JsonConvert.SerializeObject(model));
    }

    protected class AjaxResult
    {
        public bool IsRedirection { get; set; }
        public string RedirectPath { get; set; }
        public bool IsHtml { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public ApiStatusCode StatusCode { get; set; }
    }

    protected async Task<ContentResult> SuccessResultWithPageHtml(string pageName, object? pageModel)
    {
        return AjaxHtmlResult(ApiResult<string>.Success
            (await _razorToStringRenderer.RenderToStringAsync(pageName, pageModel, PageContext)));
    }
}