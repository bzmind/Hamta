namespace Shop.UI.Models.Products;

public class ReplaceProductMainImageCommandViewModel
{
    public long ProductId { get; set; }
    public IFormFile MainImage { get; set; }
}