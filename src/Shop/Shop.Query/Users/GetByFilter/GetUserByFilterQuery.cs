using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Users._DTOs;
using Shop.Query.Users._Mappers;

namespace Shop.Query.Users.GetByFilter;

public class GetUserByFilterQuery : BaseFilterQuery<UserFilterResult, UserFilterParam>
{
    public GetUserByFilterQuery(UserFilterParam filterParams) : base(filterParams)
    {
    }
}

public class GetUserByFilterQueryHandler : IBaseQueryHandler<GetUserByFilterQuery, UserFilterResult>
{
    private readonly ShopContext _shopContext;

    public GetUserByFilterQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<UserFilterResult> Handle(GetUserByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;

        var query = _shopContext.Users.OrderByDescending(c => c.Id).AsQueryable();

        if (!string.IsNullOrWhiteSpace(@params.Name))
            query = query.Where(c => c.FullName.Contains(@params.Name));

        if (!string.IsNullOrWhiteSpace(@params.PhoneNumber))
            query = query.Where(c => c.FullName.Contains(@params.PhoneNumber));

        if (!string.IsNullOrWhiteSpace(@params.Email))
            query = query.Where(c => c.FullName.Contains(@params.Email));

        var skip = (@params.PageId - 1) * @params.Take;

        var finalQuery = await query
            .Skip(skip)
            .Take(@params.Take)
            .Select(c => c.MapToUserDto())
            .ToListAsync(cancellationToken);

        var favoriteItemProductIds = new List<long>();
        finalQuery.ForEach(c =>
        {
            c.FavoriteItems.ForEach(fi =>
            {
                favoriteItemProductIds.Add(fi.ProductId);
            });
        });

        var tables = await _shopContext.Products.Where(p => favoriteItemProductIds.Contains(p.Id))
            .Join(
                _shopContext.Inventories,
                p => p.Id,
                i => i.ProductId,
                (product, inventory) => new
                {
                    product,
                    inventory
                })
            .ToListAsync(cancellationToken);

        finalQuery.ForEach(c =>
        {
            c.FavoriteItems.ForEach(fi =>
            {
                var product = tables.First(t => t.product.Id == fi.ProductId).product;
                var inventory = tables.First(t => t.inventory.Id == product.Id).inventory;
                fi.ProductName = product.Name;
                fi.ProductMainImage = product.MainImage.Name;
                fi.ProductPrice = inventory.Price.Value;
                fi.AverageScore = product.AverageScore;
                fi.IsAvailable = inventory.IsAvailable;
            });
        });

        return new UserFilterResult
        {
            Data = finalQuery,
            FilterParam = @params
        };
    }
}