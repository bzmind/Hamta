using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Shop.UI.SetupClasses.RazorUtility;

[ValidateAntiForgeryToken]
public class BaseRazorPage : PageModel
{
    protected void MakeAlert(ApiResult apiResult)
    {
        var model = JsonConvert.SerializeObject(apiResult);
        HttpContext.Response.Cookies.Append("alert", model);
    }

    protected ContentResult AjaxResultJson<T>(ApiResult<T> apiResult, bool isHtml)
    {
        var model = new AjaxResult
        {
            IsHtml = isHtml,
            Message = apiResult.MetaData.Message,
            Data = apiResult.Data,
            StatusCode = apiResult.MetaData.ApiStatusCode
        };
        return Content(JsonConvert.SerializeObject(model));
    }

    protected class AjaxResult
    {
        public bool IsHtml { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public ApiStatusCode StatusCode { get; set; }
    }
}