using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Categories;

public class CategorySpecificationViewModel
{
    [Required(ErrorMessage = ValidationMessages.CategorySpecificationIdRequired)]
    public long Id { get; set; }

    [DisplayName("عنوان")]
    [Required(ErrorMessage = ValidationMessages.TitleRequired)]
    [MaxLength(50, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Title { get; set; }

    [DisplayName("ویژگی مهم")]
    public bool IsImportant { get; set; }

    [DisplayName("ویژگی اختیاری")]
    public bool IsOptional { get; set; }
}