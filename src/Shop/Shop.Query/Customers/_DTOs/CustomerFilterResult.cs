using Common.Query.BaseClasses.FilterQuery;

namespace Shop.Query.Customers._DTOs;

public class CustomerFilterResult : BaseFilterResult<CustomerDto, CustomerFilterParam>
{
    
}

public class CustomerFilterParam : BaseFilterParam
{
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}