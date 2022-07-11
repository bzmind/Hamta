using Common.Application;

namespace Shop.API.ViewModels.Categories;

public class EditCategoryViewModel
{
    public long Id { get; set; }
    public long? ParentId { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public List<Specification>? Specifications { get; set; }
}