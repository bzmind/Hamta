using Common.Query.BaseClasses.FilterQuery;

namespace Shop.Query.Users._DTOs;

public class UserFilterResult : BaseFilterResult<UserDto, UserFilterParam>
{
    
}

public class UserFilterParam : BaseFilterParam
{
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}