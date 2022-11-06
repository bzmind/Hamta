using Common.Api.Utility;
using Microsoft.AspNetCore.Mvc;
using Shop.Query.Users._DTOs;
using Shop.UI.Services.Users;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Profile.Favorites;

public class IndexModel : BaseRazorPage
{
    private readonly IUserService _userService;

    public IndexModel(IRazorToStringRenderer razorToStringRenderer,
        IUserService userService) : base(razorToStringRenderer)
    {
        _userService = userService;
    }

    public List<UserFavoriteItemDto> FavoriteItems { get; set; }

    public async Task OnGet()
    {
        var result = await GetData(async () => await _userService.GetById(User.GetUserId()));
        FavoriteItems = result.FavoriteItems;
    }

    public async Task<IActionResult> OnPost(long favoriteItemId)
    {
        var result = await _userService.RemoveFavoriteItem(favoriteItemId);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }
        return AjaxReloadCurrentPageResult();
    }
}