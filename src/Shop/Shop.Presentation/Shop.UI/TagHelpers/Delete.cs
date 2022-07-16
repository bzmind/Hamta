using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Shop.UI.TagHelpers;

public class Delete : TagHelper
{
    public string Url { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.Attributes.Add("class", "btn btn-danger btn-sm");
        output.Attributes.Add("onclick", $"deleteItem('{Url}')");
        base.Process(context, output);
    }
}