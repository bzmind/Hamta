using Common.Application;
using MediatR;
using Shop.Application.Users.AddFavoriteItem;
using Shop.Application.Users.AddRole;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.Register;
using Shop.Application.Users.Remove;
using Shop.Application.Users.RemoveFavoriteItem;
using Shop.Application.Users.RemoveRole;
using Shop.Application.Users.SetAvatar;
using Shop.Application.Users.SetSubscriptionToNews;
using Shop.Query.Users._DTOs;
using Shop.Query.Users.GetByEmailOrPhone;
using Shop.Query.Users.GetByFilter;
using Shop.Query.Users.GetById;
using Shop.Query.Users.SearchByEmailOrPhone;

namespace Shop.Presentation.Facade.Users;

internal class UserFacade : IUserFacade
{
    private readonly IMediator _mediator;

    public UserFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult<long>> Create(CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Register(RegisterUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> SetAvatar(SetUserAvatarCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> SetSubscriptionToNews(SetUserSubscriptionToNewsCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddFavoriteItem(AddUserFavoriteItemCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveFavoriteItem(RemoveUserFavoriteItemCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddRole(AddUserRoleCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveRole(RemoveUserRoleCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Remove(long userId)
    {
        return await _mediator.Send(new RemoveUserCommand(userId));
    }

    public async Task<UserDto?> GetById(long id)
    {
        return await _mediator.Send(new GetUserByIdQuery(id));
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