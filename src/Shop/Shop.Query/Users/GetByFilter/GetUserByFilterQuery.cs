using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Users._DTOs;
using Shop.Query.Users._Mappers;

namespace Shop.Query.Users.GetByFilter;

public class GetUserByFilterQuery : BaseFilterQuery<UserFilterResult, UserFilterParams>
{
    public GetUserByFilterQuery(UserFilterParams filterParams) : base(filterParams)
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
        var @params = request.FilterFilterParams;

        var query = _shopContext.Users
            .Join(_shopContext.Avatars,
                user => user.AvatarId,
                avatar => avatar.Id,
                (user, avatar) => new { user, avatar })
            .OrderByDescending(c => c.user.Id).AsQueryable();

        if (!string.IsNullOrWhiteSpace(@params.Name))
            query = query.Where(c => c.user.FullName.Contains(@params.Name));

        if (!string.IsNullOrWhiteSpace(@params.PhoneNumber))
            query = query.Where(c => c.user.FullName.Contains(@params.PhoneNumber));

        if (!string.IsNullOrWhiteSpace(@params.Email))
            query = query.Where(c => c.user.FullName.Contains(@params.Email));

        var skip = (@params.PageId - 1) * @params.Take;

        var queryResult = await query
            .Skip(skip)
            .Take(@params.Take)
            .Select(c => c.user.MapToUserFilterDto(c.avatar))
            .ToListAsync(cancellationToken);

        var roleIds = new List<long>();
        queryResult.ForEach(user =>
        {
            user.Roles.ForEach(roleDto => roleIds.Add(roleDto.RoleId));
        });

        var roles = await _shopContext.Roles.Where(r => roleIds.Contains(r.Id)).ToListAsync(cancellationToken);

        queryResult.ForEach(user =>
        {
            user.Roles.ForEach(roleDto =>
            {
                var role = roles.First(role => role.Id == roleDto.RoleId);
                roleDto.RoleTitle = role.Title;
            });
        });

        var model = new UserFilterResult
        {
            Data = queryResult,
            FilterParams = @params
        };
        model.GeneratePaging(query.Count(), @params.Take, @params.PageId);
        return model;
    }
}