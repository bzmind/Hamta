using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Avatars;
using Shop.Query.Avatars._DTOs;
using Shop.UI.Services.Avatars;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Admin.Avatars;

[BindProperties]
public class IndexModel : BaseRazorPage
{
    private readonly IAvatarService _avatarService;

    public IndexModel(IRazorToStringRenderer razorToStringRenderer,
        IAvatarService avatarService) : base(razorToStringRenderer)
    {
        _avatarService = avatarService;
    }

    public List<AvatarDto> Avatars { get; set; }

    public async Task OnGet()
    {
        Avatars = await _avatarService.GetAll();
    }

    public async Task<IActionResult> OnPost(CreateAvatarViewModel viewModel)
    {
        var result = await _avatarService.Create(viewModel);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }

        return AjaxRedirectToPageResult();
    }

    public async Task<IActionResult> OnGetShowAddPage()
    {
        return await AjaxSuccessHtmlResultAsync("_Add", new CreateAvatarViewModel());
    }

    public async Task<IActionResult> OnPostRemoveAvatar(long avatarId)
    {
        var result = await _avatarService.Remove(avatarId);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }

        return AjaxRedirectToPageResult();
    }
}