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
            .Select(c => c.MapToUserFilterDto())
            .ToListAsync(cancellationToken);

        var roleIds = new List<long>();
        finalQuery.ForEach(u =>
        {
            u.Roles.ForEach(r => roleIds.Add(r.RoleId));
        });

        var roles = await _shopContext.Roles.Where(r => roleIds.Contains(r.Id)).ToListAsync(cancellationToken);

        finalQuery.ForEach(u =>
        {
            u.Roles.ForEach(r =>
            {
                var role = roles.First(rl => rl.Id == r.RoleId);
                r.RoleTitle = role.Title;
            });
        });

        return new UserFilterResult
        {
            Data = finalQuery,
            FilterParam = @params
        };
    }
}