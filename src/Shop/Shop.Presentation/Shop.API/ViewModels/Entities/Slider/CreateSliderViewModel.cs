using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;
using Common.Application.Utility.Validation.CustomAttributes;

namespace Shop.API.ViewModels.Entities.Slider;

public class CreateSliderViewModel
{
    [DisplayName("عنوان")]
    [Required(ErrorMessage = ValidationMessages.TitleRequired)]
    [MaxLength(50, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Title { get; set; }

    [DisplayName("لینک")]
    [Required(ErrorMessage = ValidationMessages.LinkRequired)]
    [MaxLength(500, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    [DataType(DataType.Url, ErrorMessage = ValidationMessages.InvalidLink)]
    public string Link { get; set; }

    [DisplayName("عکس اسلایدر")]
    [Required(ErrorMessage = ValidationMessages.SliderImageRequired)]
    [ImageFile(ErrorMessage = ValidationMessages.InvalidSliderImage)]
    public IFormFile Image { get; set; }
}