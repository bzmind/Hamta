using Common.Application;

namespace Shop.UI.Models.Categories;

public class AddSubCategoryViewModel
{
    public int ParentId { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public List<Specification> Specifications { get; set; }
}