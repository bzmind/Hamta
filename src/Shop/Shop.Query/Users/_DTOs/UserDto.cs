﻿using Common.Domain.ValueObjects;
using Common.Query.BaseClasses;

namespace Shop.Query.Users._DTOs;

public class UserDto : BaseDto
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<UserAddressDto> Addresses { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public string AvatarName { get; set; }
    public bool IsSubscribedToNews { get; set; }
    public List<UserFavoriteItemDto> FavoriteItems { get; set; }
    public List<UserRoleDto> Roles { get; set; }
}