using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Customers._DTOs;
using Shop.Query.Customers._Mappers;

namespace Shop.Query.Customers.GetById;

public record GetCustomerByIdQuery(long CustomerId) : IBaseQuery<CustomerDto?>;

public class GetCustomerByIdQueryHandler : IBaseQueryHandler<GetCustomerByIdQuery, CustomerDto?>
{
    private readonly ShopContext _shopContext;
    private readonly DapperContext _dapperContext;

    public GetCustomerByIdQueryHandler(ShopContext shopContext, DapperContext dapperContext)
    {
        _shopContext = shopContext;
        _dapperContext = dapperContext;
    }

    public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _shopContext.Customers.FirstOrDefaultAsync(c => c.Id == request.CustomerId, cancellationToken);
        
        if (customer == null)
            return null;

        var customerDto = customer.MapToCustomerDto();
        var favoriteItems = await customerDto.GetFavoriteItemsAsDto(_dapperContext);
        customerDto.FavoriteItems = favoriteItems;
        return customerDto;
    }
}