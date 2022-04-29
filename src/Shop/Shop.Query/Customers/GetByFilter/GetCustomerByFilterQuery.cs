using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Customers._DTOs;
using Shop.Query.Customers._Mappers;

namespace Shop.Query.Customers.GetByFilter;

public class GetCustomerByFilterQuery : BaseFilterQuery<CustomerFilterResult, CustomerFilterParam>
{
    public GetCustomerByFilterQuery(CustomerFilterParam filterParams) : base(filterParams)
    {
    }
}

public class GetCustomerByFilterQueryHandler : IBaseQueryHandler<GetCustomerByFilterQuery, CustomerFilterResult>
{
    private readonly ShopContext _shopContext;

    public GetCustomerByFilterQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<CustomerFilterResult> Handle(GetCustomerByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;

        var query = _shopContext.Customers.OrderByDescending(c => c.Id).AsQueryable();

        if (!string.IsNullOrWhiteSpace(@params.Name))
            query = query.Where(c => c.FullName.Contains(@params.Name));

        if (!string.IsNullOrWhiteSpace(@params.PhoneNumber))
            query = query.Where(c => c.FullName.Contains(@params.PhoneNumber));

        if (!string.IsNullOrWhiteSpace(@params.Email))
            query = query.Where(c => c.FullName.Contains(@params.Email));

        var skip = (@params.PageId - 1) * @params.Take;

        return new CustomerFilterResult
        {
            Data = await query
                .Skip(skip)
                .Take(@params.Take)
                .Select(c => c.MapToCustomerDto())
                .ToListAsync(cancellationToken),

            FilterParam = @params
        };
    }
}