using System.ComponentModel;
using Shop.API.ViewModels;

namespace Shop.UI.ViewModels.Categories;

public class CategoryViewModel
{
    [DisplayName("آیدی")]
    public long Id { get; set; }

    [DisplayName("تاریخ ایجاد")]
    public DateTime CreationDate { get; set; }

    [DisplayName("آیدی والد")]
    public long? ParentId { get; set; }

    [DisplayName("عنوان")]
    public string Title { get; set; }

    [DisplayName("اسلاگ")]
    public string Slug { get; set; }

    [DisplayName("زیرگروه‌ها")]
    public List<CategoryViewModel> SubCategories { get; set; }

    [DisplayName("مشخصات")]
    public List<SpecificationViewModel> Specifications { get; set; }
}