using Common.Query.BaseClasses.FilterQuery;

namespace Shop.Query.Users._DTOs;

public class UserFilterResult : BaseFilterResult<UserFilterDto, UserFilterParams>
{
}

public class UserFilterParams : BaseFilterParams
{
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}