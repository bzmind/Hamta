using Common.Api.Utility;
using Shop.Query.Orders._DTOs;
using Shop.Query.Users._DTOs;
using Shop.UI.Services.Orders;
using Shop.UI.Services.Users;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Profile;

public class IndexModel : BaseRazorPage
{
    private readonly IUserService _userService;
    private readonly IOrderService _orderService;

    public IndexModel(IRazorToStringRenderer razorToStringRenderer,
        IUserService userService, IOrderService orderService) : base(razorToStringRenderer)
    {
        _userService = userService;
        _orderService = orderService;
    }

    public UserDto UserDto { get; set; }
    public List<UserFavoriteItemDto> FavoriteItems { get; set; }
    public OrderFilterResult Orders { get; set; }

    public async Task OnGet()
    {
        UserDto = await GetData(async () => await _userService.GetById(User.GetUserId()));
        FavoriteItems = UserDto.FavoriteItems;
        Orders = await GetData(async () => await _orderService.GetByFilterForUser(1, 10, null));
    }
}