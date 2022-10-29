using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Shop.UI.Setup.RazorUtility;

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

    protected void MakeErrorAlert(string message)
    {
        var apiResult = new ApiResult { IsSuccessful = false, MetaData = new MetaData { Message = message } };
        var model = JsonConvert.SerializeObject(apiResult);
        HttpContext.Response.Cookies.Append("alert", model);
    }

    protected void MakeSuccessAlert(string message)
    {
        var apiResult = new ApiResult { IsSuccessful = true, MetaData = new MetaData { Message = message } };
        var model = JsonConvert.SerializeObject(apiResult);
        HttpContext.Response.Cookies.Append("alert", model);
    }

    protected async Task<ContentResult> AjaxHtmlSuccessResultAsync(string pageName, object? pageModel)
    {
        var successApiResult = ApiResult<string>.Success
            (await _razorToStringRenderer.RenderToStringAsync(pageName, pageModel, PageContext));

        var model = new AjaxResult
        {
            IsHtml = true,
            Message = successApiResult.MetaData.Message,
            Data = successApiResult.Data,
            StatusCode = successApiResult.MetaData.ApiStatusCode
        };

        return Content(JsonConvert.SerializeObject(model));
    }

    protected ContentResult AjaxErrorMessageResult(string errorMessage, ApiStatusCode statusCode = default)
    {
        var errorApiResult = ApiResult<string>.Error(errorMessage);
        if (statusCode != default)
            errorApiResult.MetaData.ApiStatusCode = statusCode;

        var model = new AjaxResult
        {
            Message = errorApiResult.MetaData.Message,
            StatusCode = errorApiResult.MetaData.ApiStatusCode
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

    protected ContentResult AjaxRedirectToPageResult(string page = "Index")
    {
        var path = Url.PageLink(page);
        if (string.IsNullOrWhiteSpace(path))
            path = $"..{page}";

        var result = new AjaxResult
        {
            IsRedirection = true,
            RedirectPath = path
        };

        return Content(JsonConvert.SerializeObject(result));
    }

    protected ContentResult AjaxReloadCurrentPageResult()
    {
        var result = new AjaxResult
        {
            ReloadCurrentPage = true
        };

        return Content(JsonConvert.SerializeObject(result));
    }

    protected ContentResult AjaxDataSuccessResult<TData>(ApiResult<TData> apiResult)
    {
        var model = new AjaxResult
        {
            Data = apiResult.Data,
            Message = apiResult.MetaData.Message,
            StatusCode = apiResult.MetaData.ApiStatusCode
        };
        return Content(JsonConvert.SerializeObject(model));
    }

    protected ContentResult AjaxSuccessResult(ApiResult? apiResult = null)
    {
        var successApiResult = apiResult ?? ApiResult.Success();
        var model = new AjaxResult
        {
            Message = successApiResult.MetaData.Message,
            StatusCode = successApiResult.MetaData.ApiStatusCode
        };
        return Content(JsonConvert.SerializeObject(model));
    }

    protected async Task<T> GetData<T>(Func<Task<ApiResult<T>>> func)
    {
        var result = await func();
        if (!result.IsSuccessful || result.Data == null)
            MakeAlert(result);
        return result.Data;
    }

    protected class AjaxResult
    {
        public bool IsRedirection { get; set; }
        public bool ReloadCurrentPage { get; set; }
        public string RedirectPath { get; set; }
        public bool IsHtml { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public ApiStatusCode StatusCode { get; set; }
    }
}