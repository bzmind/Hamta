using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels;

public class SpecificationViewModel
{
    [DisplayName("عنوان")]
    [Required(ErrorMessage = ValidationMessages.TitleRequired)]
    [MaxLength(50, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Title { get; set; }

    [DisplayName("توضیحات")]
    [Required(ErrorMessage = ValidationMessages.DescriptionRequired)]
    [MaxLength(300, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Description { get; set; }

    [DisplayName("ویژگی مهم")]
    public bool IsImportantFeature { get; set; }
}