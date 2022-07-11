using Common.Application;

namespace Shop.API.ViewModels.Categories;

public class AddSubCategoryViewModel
{
    public long ParentId { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public List<Specification>? Specifications { get; set; }
}