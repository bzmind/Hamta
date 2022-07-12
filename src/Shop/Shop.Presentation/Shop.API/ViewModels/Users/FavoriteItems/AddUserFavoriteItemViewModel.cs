using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Users.FavoriteItems;

public class AddUserFavoriteItemViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long UserId { get; set; }

    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long ProductId { get; set; }
}