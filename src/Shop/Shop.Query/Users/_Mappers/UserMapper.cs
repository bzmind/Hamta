using Dapper;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.AvatarAggregate;
using Shop.Domain.UserAggregate;
using Shop.Infrastructure;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Users._DTOs;

namespace Shop.Query.Users._Mappers;

internal static class UserMapper
{
    public static UserDto? MapToUserDto(this User? user, Avatar avatar)
    {
        if (user == null)
            return null;

        var userDto = new UserDto
        {
            Id = user.Id,
            CreationDate = user.CreationDate,
            FullName = user.FullName,
            Email = user.Email,
            Password = user.Password,
            Addresses = user.Addresses.ToList().MapToUserAddressDto(),
            PhoneNumber = user.PhoneNumber,
            Gender = user.Gender,
            Avatar = new AvatarDto
            {
                Id = avatar.Id,
                CreationDate = avatar.CreationDate,
                Name = avatar.Name,
                Gender = avatar.Gender
            },
            IsSubscribedToNewsletter = user.IsSubscribedToNewsletter,
            FavoriteItems = user.FavoriteItems.Select(fi => new UserFavoriteItemDto
            {
                Id = fi.Id,
                CreationDate = fi.CreationDate,
                UserId = fi.UserId,
                ProductId = fi.ProductId
            }).ToList(),
            Roles = user.Roles.Select(r => new UserRoleDto
            {
                Id = r.Id,
                CreationDate = r.CreationDate,
                RoleId = r.RoleId,
                RoleTitle = ""
            }).ToList()
        };

        return userDto;
    }

    public static UserFilterDto? MapToUserFilterDto(this User? user, Avatar avatar)
    {
        if (user == null)
            return null;

        var userDto = new UserFilterDto
        {
            Id = user.Id,
            CreationDate = user.CreationDate,
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Gender = user.Gender,
            AvatarName = avatar.Name,
            IsSubscribedToNewsletter = user.IsSubscribedToNewsletter,
            Roles = user.Roles.Select(r => new UserRoleDto
            {
                Id = r.Id,
                CreationDate = r.CreationDate,
                RoleId = r.RoleId,
                RoleTitle = ""
            }).ToList()
        };

        return userDto;
    }

    public static async Task<UserDto> GetFavoriteItemsDto(this UserDto userDto, DapperContext dapperContext)
    {
        using var connection = dapperContext.CreateConnection();
        var sql = $@"
            SELECT
                fi.Id, fi.UserId, fi.ProductId, p.Name AS ProductName, p.MainImage AS ProductMainImage,
                p.Slug AS ProductSlug, i.Id AS InventoryId, i.Price AS Price, i.DiscountPercentage,
                AVG(c.Score) OVER (PARTITION BY p.Id) AS AverageScore, i.IsAvailable
            FROM {dapperContext.UserFavoriteItems} fi
            LEFT JOIN {dapperContext.Products} p
                ON p.id = fi.ProductId
            LEFT JOIN {dapperContext.Comments} c
                ON c.ProductId = p.Id
            LEFT JOIN {dapperContext.SellerInventories} i
                ON i.ProductId = fi.ProductId
            WHERE fi.UserId = @UserDtoId";

        var result = await connection.QueryAsync<UserFavoriteItemDto>(sql, new { UserDtoId = userDto.Id });

        var groupedItems = result.GroupBy(i => i.ProductId).Select(itemsGroup =>
        {
            var firstItem = itemsGroup.OrderBy(p => p.TotalDiscountedPrice).First();
            firstItem.AverageScore = itemsGroup.OrderBy(p => p.TotalDiscountedPrice)
                .First().AverageScore;
            return firstItem;
        }).ToList();

        userDto.FavoriteItems = groupedItems;
        return userDto;
    }

    public static async Task<UserDto> GetRolesDto(this UserDto userDto, ShopContext shopContext)
    {
        var roleIds = userDto.Roles.Select(r => r.RoleId);
        var roles = await shopContext.Roles.Where(r => roleIds.Contains(r.Id)).ToListAsync();

        userDto.Roles = roles.Select(r => new UserRoleDto
        {
            Id = r.Id,
            CreationDate = r.CreationDate,
            RoleId = r.Id,
            RoleTitle = r.Title
        }).ToList();

        return userDto;
    }
}