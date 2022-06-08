using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Roles._DTOs;
using Shop.Query.Roles._Mapper;

namespace Shop.Query.Roles.GetList;

public record GetRoleListQuery : IBaseQuery<List<RoleDto>>;

public class GetRoleListQueryHandler : IBaseQueryHandler<GetRoleListQuery, List<RoleDto>>
{
    private readonly ShopContext _shopContext;

    public GetRoleListQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<List<RoleDto>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
    {
        var roles = await _shopContext.Roles.ToListAsync(cancellationToken);
        return roles.MapToRoleDto();
    }
}