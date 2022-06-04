using Shop.Domain.UserAggregate;
using Shop.Query.Users._DTOs;

namespace Shop.Query.Users._Mappers;

internal static class UserMapper
{
    public static UserDto MapToUserDto(this User? user)
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
            AvatarName = user.AvatarName,
            IsSubscribedToNews = user.IsSubscribedToNews,
            FavoriteItems = new List<UserFavoriteItemDto>()
        };

        user.FavoriteItems.ToList().ForEach(fi =>
        {
            userDto.FavoriteItems.Add(new UserFavoriteItemDto
            {
                Id = fi.Id,
                CreationDate = fi.CreationDate,
                UserId = fi.UserId,
                ProductId = fi.ProductId,
                ProductName = null,
                ProductMainImage = null,
                ProductPrice = null,
                AverageScore = null,
                IsAvailable = null
            });
        });

        return userDto;
    }
}