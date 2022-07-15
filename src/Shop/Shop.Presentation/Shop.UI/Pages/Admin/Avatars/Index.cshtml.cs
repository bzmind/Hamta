using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
}