using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Products;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Admin.Products;

[BindProperties]
public class AddModel : BaseRazorPage
{
    public AddModel(IRazorToStringRenderer razorToStringRenderer) : base(razorToStringRenderer)
    {
    }

    public CreateProductViewModel CreateProductViewModel { get; set; } = new();

    public void OnGet()
    {
    }
}