using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Api.Attributes;
using Common.Application.Utility.Validation;
using Common.Application.Utility.Validation.CustomAttributes;

namespace Shop.API.ViewModels.Products;

public class CreateProductViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
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

    [DisplayName("توضیحات")]
    [Required(ErrorMessage = ValidationMessages.DescriptionRequired)]
    [MaxLength(2000, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string? Description { get; set; }

    [DisplayName("عکس اصلی محصول")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [ImageFile(ErrorMessage = "عکس اصلی محصول نامعتبر است")]
    public IFormFile MainImage { get; set; }

    [DisplayName("عکس های گالری محصول")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [ListNotEmpty(ErrorMessage = "لطفا عکس های گالری محصول را وارد کنید")]
    [ImageFile(ErrorMessage = "عکس های گالری محصول نامعتبر هستند")]
    [ListMaxLength(10, ErrorMessage = "عکس های گالری محصول نمی‌تواند بیشتر از 10 عدد باشد")]
    public List<IFormFile> GalleryImages { get; set; }

    [DisplayName("مشخصات")]
    public List<SpecificationViewModel>? CustomSpecifications { get; set; }

    [DisplayName("توضیحات اضافه")]
    [DictionaryMembersCharactersMaxLength(100, 2000, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public Dictionary<string, string>? ExtraDescriptions { get; set; }
}