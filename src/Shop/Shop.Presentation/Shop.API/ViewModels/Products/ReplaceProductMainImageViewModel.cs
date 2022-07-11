namespace Shop.API.ViewModels.Products;

public class ReplaceProductMainImageViewModel
{
    public long ProductId { get; set; }
    public IFormFile MainImage { get; set; }
}