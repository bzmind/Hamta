using Common.Application;

namespace Shop.API.ViewModels.Products;

public class EditProductViewModel
{
    public long ProductId { get; set; }
    public long CategoryId { get; set; }
    public string Name { get; set; }
    public string? EnglishName { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public IFormFile? MainImage { get; set; }
    public List<IFormFile>? GalleryImages { get; set; }
    public List<Specification>? CustomSpecifications { get; set; }
    public Dictionary<string, string>? ExtraDescriptions { get; set; }
}