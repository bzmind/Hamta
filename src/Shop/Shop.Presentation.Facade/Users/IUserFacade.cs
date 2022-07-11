using Common.Application;
using Shop.Application.Users.Auth.Register;
using Shop.Application.Users.Auth.ResetPassword;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.FavoriteItems.AddFavoriteItem;
using Shop.Application.Users.FavoriteItems.RemoveFavoriteItem;
using Shop.Application.Users.Roles.AddRole;
using Shop.Application.Users.Roles.RemoveRole;
using Shop.Query.Users._DTOs;

namespace Shop.Presentation.Facade.Users;

public interface IUserFacade
{
    Task<OperationResult<long>> Create(CreateUserCommand command);
    Task<OperationResult> Edit(EditUserCommand command);
    Task<OperationResult> Register(RegisterUserCommand command);
    Task<OperationResult> ResetPassword(ResetUserPasswordCommand command);
    Task<OperationResult<bool>> SetNewsletterSubscription(long userId);
    Task<OperationResult> SetAvatar(long userId, long avatarId);
    Task<OperationResult> AddFavoriteItem(AddUserFavoriteItemCommand command);
    Task<OperationResult> RemoveFavoriteItem(RemoveUserFavoriteItemCommand command);
    Task<OperationResult> AddRole(AddUserRoleCommand command);
    Task<OperationResult> RemoveRole(RemoveUserRoleCommand command);
    Task<OperationResult> Remove(long userId);

    Task<UserDto?> GetById(long id);
    Task<UserDto?> GetByEmailOrPhone(string emailOrPhone);
    Task<LoginNextStep> SearchByEmailOrPhone(string emailOrPhone);
    Task<UserFilterResult> GetByFilter(UserFilterParams filterParams);
}