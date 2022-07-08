using Common.Application;
using MediatR;
using Shop.Application.Avatars.Create;
using Shop.Application.Avatars.Remove;
using Shop.Domain.AvatarAggregate;
using Shop.Query.Avatars._DTOs;
using Shop.Query.Avatars.GetAll;
using Shop.Query.Avatars.GetByGender;
using Shop.Query.Avatars.GetById;

namespace Shop.Presentation.Facade.Avatars;

public class AvatarFacade : IAvatarFacade
{
    private readonly IMediator _mediator;

    public AvatarFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult<long>> Create(CreateAvatarCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Remove(long avatarId)
    {
        return await _mediator.Send(new RemoveAvatarCommand(avatarId));
    }

    public async Task<AvatarDto?> GetById(long avatarId)
    {
        return await _mediator.Send(new GetAvatarByIdQuery(avatarId));
    }

    public async Task<List<AvatarDto>> GetAll()
    {
        return await _mediator.Send(new GetAvatarsListQuery());
    }

    public async Task<AvatarDto?> GetByGender(Avatar.AvatarGender gender)
    {
        return await _mediator.Send(new GetAvatarByGenderQuery(gender));
    }
}