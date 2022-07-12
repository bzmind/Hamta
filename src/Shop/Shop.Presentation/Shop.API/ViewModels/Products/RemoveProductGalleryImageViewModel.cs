using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Products;

public class RemoveProductGalleryImageViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long ProductId { get; init; }

    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long GalleryImageId { get; init; }
}