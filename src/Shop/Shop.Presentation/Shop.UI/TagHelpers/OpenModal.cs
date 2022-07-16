using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Shop.UI.TagHelpers;

public class OpenModal : TagHelper
{
    public string Url { get; set; }
    public string ModalTitle { get; set; } = "";
    public string ModalSize { get; set; } = "modal-md";

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.Attributes.Add("onclick", $"openModal('{Url}', '{ModalTitle}', '{ModalSize}')");
        base.Process(context, output);
    }
}