using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Api.Attributes;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Products;

public class ProductCategorySpecificationViewModel
{
    [Required(ErrorMessage = ValidationMessages.ChooseCategorySpecification)]
    public long CategorySpecificationId { get; set; }

    [DisplayName("عنوان")]
    public string Title { get; set; }

    [DisplayName("توضیحات")]
    [RequiredIf(nameof(IsOptional), false, ErrorMessage = ValidationMessages.DescriptionRequired)]
    [MaxLength(300, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string? Description { get; set; }

    [DisplayName("ویژگی مهم")]
    public bool IsImportant { get; set; }

    [DisplayName("ویژگی اختیاری")]
    public bool IsOptional { get; set; }
}