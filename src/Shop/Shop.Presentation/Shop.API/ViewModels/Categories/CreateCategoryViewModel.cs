using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Categories;

public class CreateCategoryViewModel
{
    [DisplayName("عنوان")]
    [Required(ErrorMessage = ValidationMessages.TitleRequired)]
    [MaxLength(50, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Title { get; set; }

    [DisplayName("اسلاگ")]
    [Required(ErrorMessage = ValidationMessages.TitleRequired)]
    [MaxLength(100, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Slug { get; set; }

    [DisplayName("مشخصات")]
    public List<SpecificationViewModel>? Specifications { get; set; }
}