using Shop.Query.Users._DTOs;
using Shop.UI.Services.Users;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Admin.Users;

public class IndexModel : BaseRazorPage
{
    private readonly IUserService _userService;

    public IndexModel(IUserService userService,
        IRazorToStringRenderer razorToStringRenderer) : base(razorToStringRenderer)
    {
        _userService = userService;
    }

    public UserFilterResult UserFilterResult { get; set; }

    public async Task OnGet()
    {
        UserFilterResult = await GetData(async () => await _userService.GetByFilter(new UserFilterParams()));
    }
}