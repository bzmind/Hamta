using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Shop.UI.TagHelpers;

public class Cancel : TagHelper
{
    private readonly IHttpContextAccessor _contextAccessor;

    public Cancel(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string BackUrl { get; set; }
    public string Text { get; set; } = "انصراف";

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var backUrl = GetRefererUrl();
        output.TagName = "a";
        output.Attributes.Add("href", BackUrl ?? backUrl);
        output.Attributes.Add("class", "btn btn-danger");
        output.Content.SetContent(Text);
        base.Process(context, output);
    }

    private string GetRefererUrl()
    {
        var backUrl = _contextAccessor.HttpContext.Request.Headers["Referer"];
        if (string.IsNullOrWhiteSpace(backUrl))
            backUrl = "/";
        return backUrl;
    }
}