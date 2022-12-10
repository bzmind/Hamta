using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Api.Attributes;
using Common.Application.Utility.Validation;
using Common.Application.Utility.Validation.CustomAttributes;

namespace Shop.API.ViewModels.Entities.Banner;

public class EditBannerViewModel
{
    [Required(ErrorMessage = ValidationMessages.ChooseBanner)]
    public long Id { get; set; }

    [DisplayName("لینک")]
    [Required(ErrorMessage = ValidationMessages.LinkRequired)]
    [MaxLength(500, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    [Uri(ErrorMessage = ValidationMessages.InvalidLink)]
    public string Link { get; set; }

    [DisplayName("عکس بنر")]
    [ImageFile(ErrorMessage = ValidationMessages.InvalidBannerImage)]
    public IFormFile? Image { get; set; }

    public string? PreviousImageName { get; set; }

    [DisplayName("موقعیت بنر")]
    [Required(ErrorMessage = ValidationMessages.ChooseBannerPosition)]
    public Domain.Entities.Banner.BannerPosition Position { get; set; }
}