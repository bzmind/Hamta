using Common.Application;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shop.Application.Users.Auth.Register;
using Shop.Application.Users.Auth.ResetPassword;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.FavoriteItems.AddFavoriteItem;
using Shop.Application.Users.FavoriteItems.RemoveFavoriteItem;
using Shop.Application.Users.Remove;
using Shop.Application.Users.Roles.AddRole;
using Shop.Application.Users.Roles.RemoveRole;
using Shop.Application.Users.SetAvatar;
using Shop.Application.Users.SetNewsletterSubscription;
using Shop.Presentation.Facade.Caching;
using Shop.Query.Users._DTOs;
using Shop.Query.Users.GetByEmailOrPhone;
using Shop.Query.Users.GetByFilter;
using Shop.Query.Users.GetById;
using Shop.Query.Users.SearchByEmailOrPhone;

namespace Shop.Presentation.Facade.Users;

internal class UserFacade : IUserFacade
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;

    public UserFacade(IMediator mediator, IDistributedCache cache)
    {
        _mediator = mediator;
        _cache = cache;
    }

    public async Task<OperationResult<long>> Create(CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditUserCommand command)
    {
        await _cache.RemoveAsync(CacheKeys.User(command.UserId));
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Register(RegisterUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> ResetPassword(ResetUserPasswordCommand command)
    {
        await _cache.RemoveAsync(CacheKeys.User(command.UserId));
        return await _mediator.Send(command);
    }

    public async Task<OperationResult<bool>> SetNewsletterSubscription(long userId)
    {
        await _cache.RemoveAsync(CacheKeys.User(userId));
        return await _mediator.Send(new SetUserNewsletterSubscriptionCommand(userId));
    }

    public async Task<OperationResult> SetAvatar(long userId, long avatarId)
    {
        await _cache.RemoveAsync(CacheKeys.User(userId));
        return await _mediator.Send(new SetUserAvatarCommand(userId, avatarId));
    }

    public async Task<OperationResult> AddFavoriteItem(AddUserFavoriteItemCommand command)
    {
        await _cache.RemoveAsync(CacheKeys.User(command.UserId));
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveFavoriteItem(RemoveUserFavoriteItemCommand command)
    {
        await _cache.RemoveAsync(CacheKeys.User(command.UserId));
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddRole(AddUserRoleCommand command)
    {
        await _cache.RemoveAsync(CacheKeys.User(command.UserId));
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveRole(RemoveUserRoleCommand command)
    {
        await _cache.RemoveAsync(CacheKeys.User(command.UserId));
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Remove(long userId)
    {
        return await _mediator.Send(new RemoveUserCommand(userId));
    }

    public async Task<UserDto?> GetById(long id)
    {
        return await _cache.GetOrSet(CacheKeys.User(id),
            async () => await _mediator.Send(new GetUserByIdQuery(id)));
    }

    public async Task<UserDto?> GetByEmailOrPhone(string emailOrPhone)
    {
        return await _mediator.Send(new GetUserByEmailOrPhoneQuery(emailOrPhone));
    }

    public async Task<LoginNextStep> SearchByEmailOrPhone(string emailOrPhone)
    {
        return await _mediator.Send(new SearchUserByEmailOrPhoneQuery(emailOrPhone));
    }

    public async Task<UserFilterResult> GetByFilter(UserFilterParams filterParams)
    {
        return await _mediator.Send(new GetUserByFilterQuery(filterParams));
    }
}