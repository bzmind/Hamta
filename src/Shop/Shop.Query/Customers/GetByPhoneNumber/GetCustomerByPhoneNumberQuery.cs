using Common.Domain.ValueObjects;
using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Customers._DTOs;
using Shop.Query.Customers._Mappers;

namespace Shop.Query.Customers.GetByPhoneNumber;

public record GetCustomerByPhoneNumberQuery(string PhoneNumber) : IBaseQuery<CustomerDto?>;

public class GetCustomerByPhoneNumberQueryHandler : IBaseQueryHandler<GetCustomerByPhoneNumberQuery, CustomerDto?>
{
    private readonly ShopContext _shopContext;

    public GetCustomerByPhoneNumberQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<CustomerDto?> Handle(GetCustomerByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        var phoneNumber = new PhoneNumber(request.PhoneNumber);

        var customer =
            await _shopContext.Customers.FirstOrDefaultAsync(c => c.PhoneNumber.Value == phoneNumber.Value,
                cancellationToken);

        return customer.MapToCustomerDto();
    }
}