using Common.Application;

namespace Shop.UI.Models.Categories;

public class CreateCategoryCommandViewModel
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public List<Specification> Specifications { get; set; }
}