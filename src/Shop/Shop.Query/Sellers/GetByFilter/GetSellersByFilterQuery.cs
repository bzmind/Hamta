using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Sellers._DTOs;
using Shop.Query.Sellers._Mappers;

namespace Shop.Query.Sellers.GetByFilter;

public class GetSellersByFilterQuery : BaseFilterQuery<SellerFilterResult, SellerFilterParams>
{
    public GetSellersByFilterQuery(SellerFilterParams filterParams) : base(filterParams)
    {
    }
}

public class GetSellersByFilterQueryHandler : IBaseQueryHandler<GetSellersByFilterQuery, SellerFilterResult>
{
    private readonly ShopContext _context;

    public GetSellersByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<SellerFilterResult> Handle(GetSellersByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterFilterParams;

        var query = _context.Sellers
            .OrderByDescending(seller => seller.CreationDate).AsQueryable();

        if (!string.IsNullOrWhiteSpace(@params.ShopName))
            query = query.Where(seller => seller.ShopName.Contains(@params.ShopName));

        if (!string.IsNullOrWhiteSpace(@params.NationalCode))
            query = query.Where(seller => seller.NationalCode.Contains(@params.NationalCode));

        if (@params.Status != null)
            query = query.Where(seller => seller.Status == @params.Status);
        
        var skip = (@params.PageId - 1) * @params.Take;

        var queryResult = await query
            .Skip(skip)
            .Take(@params.Take)
            .Select(seller => seller.MapToSellerDto())
            .ToListAsync(cancellationToken);

        var model = new SellerFilterResult
        {
            Data = queryResult,
            FilterParams = @params
        };
        model.GeneratePaging(query.Count(), @params.Take, @params.PageId);
        return model;
    }
}