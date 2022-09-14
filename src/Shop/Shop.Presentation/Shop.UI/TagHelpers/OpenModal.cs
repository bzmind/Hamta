using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Shop.UI.TagHelpers;

public class OpenModal : TagHelper
{
    public string Url { get; set; }
    public string Title { get; set; } = "";
    public string Size { get; set; } = "modal-md";
    public string Backdrop { get; set; } = "dark";

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.Attributes.Add("onclick", $"openModal('{Url}', '{Title}', '{Size}', '{Backdrop}')");
        base.Process(context, output);
    }
}