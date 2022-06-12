using Shop.UI.Models._Filters;

namespace Shop.UI.Models.Users;

public class UserFilterParamsViewModel : BaseFilterParamsViewModel
{
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}