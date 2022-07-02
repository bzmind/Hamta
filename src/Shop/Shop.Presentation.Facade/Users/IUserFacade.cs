using Common.Application;
using Shop.Application.Users.AddFavoriteItem;
using Shop.Application.Users.AddRole;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.Register;
using Shop.Application.Users.RemoveFavoriteItem;
using Shop.Application.Users.RemoveRole;
using Shop.Application.Users.ResetPassword;
using Shop.Application.Users.SetAvatar;
using Shop.Query.Users._DTOs;

namespace Shop.Presentation.Facade.Users;

public interface IUserFacade
{
    Task<OperationResult<long>> Create(CreateUserCommand command);
    Task<OperationResult> Edit(EditUserCommand command);
    Task<OperationResult> Register(RegisterUserCommand command);
    Task<OperationResult> SetAvatar(SetUserAvatarCommand command);
    Task<OperationResult> ResetPassword(ResetUserPasswordCommand command);
    Task<OperationResult<bool>> SetNewsletterSubscription(long userId);
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