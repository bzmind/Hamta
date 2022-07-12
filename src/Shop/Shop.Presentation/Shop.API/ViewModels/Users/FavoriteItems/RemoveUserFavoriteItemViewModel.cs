using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Users.FavoriteItems;

public class RemoveUserFavoriteItemViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long UserId { get; set; }

    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long FavoriteItemId { get; set; }
}