using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Api.Attributes;
using Common.Application.Utility.Validation;
using Common.Application.Utility.Validation.CustomAttributes;

namespace Shop.API.ViewModels.Products;

public class CreateProductViewModel
{
    [Required(ErrorMessage = ValidationMessages.ChooseCategory)]
    public long CategoryId { get; set; }

    [Display(Name = "نام محصول")]
    [Required(ErrorMessage = ValidationMessages.ProductNameRequired)]
    [MaxLength(50, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Name { get; set; }

    [Display(Name = "نام انگلیسی محصول")]
    [MaxLength(50, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string? EnglishName { get; set; }

    [DisplayName("اسلاگ")]
    [Required(ErrorMessage = ValidationMessages.SlugRequired)]
    [MaxLength(100, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Slug { get; set; }

    [DisplayName("معرفی")]
    [MaxLength(2000, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    [DataType(DataType.MultilineText)]
    public string? Introduction { get; set; }

    [DisplayName("بررسی تخصصی")]
    [MaxLength(10000, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    [DataType(DataType.MultilineText)]
    public string? Review { get; set; }

    [DisplayName("عکس اصلی محصول")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [ImageFile(ErrorMessage = "عکس اصلی محصول نامعتبر است")]
    public IFormFile MainImage { get; set; }

    [DisplayName("عکس های گالری محصول")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [ListNotEmpty(ErrorMessage = "لطفا عکس های گالری محصول را وارد کنید")]
    [ImageFile(ErrorMessage = "عکس های گالری محصول نامعتبر هستند")]
    public List<IFormFile> GalleryImages { get; set; }

    [DisplayName("مشخصات")]
    public List<ProductSpecificationViewModel>? Specifications { get; set; } = new();

    [DisplayName("مشخصات دسته‌بندی")]
    public List<ProductCategorySpecificationViewModel>? CategorySpecifications { get; set; } = new();
}

public class ControllerCreateProductViewModel
{
    public long CategoryId { get; set; }
    public string Name { get; set; }
    public string? EnglishName { get; set; }
    public string Slug { get; set; }
    public string? Introduction { get; set; }
    public string? Review { get; set; }
    public IFormFile MainImage { get; set; }
    public List<IFormFile> GalleryImages { get; set; }
    public string? SpecificationsJson { get; set; }
    public string? CategorySpecificationsJson { get; set; }
}