using Common.Api.Utility;
using Shop.Query.Users._DTOs;
using Shop.UI.Services.UserAddresses;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Profile.Addresses;

public class IndexModel : BaseRazorPage
{
    private readonly IUserAddressService _userAddressService;

    public IndexModel(IRazorToStringRenderer razorToStringRenderer,
        IUserAddressService userAddressService) : base(razorToStringRenderer)
    {
        _userAddressService = userAddressService;
    }

    public List<UserAddressDto> Addresses { get; set; }

    public async Task OnGet()
    {
        Addresses = await _userAddressService.GetAll(User.GetUserId());
    }
}