using Common.Application;
using MediatR;
using Shop.Application.Entities.Banners.Create;
using Shop.Application.Entities.Banners.Delete;
using Shop.Application.Entities.Banners.Edit;
using Shop.Query.Entities._DTOs;
using Shop.Query.Entities.Banners.GetAll;
using Shop.Query.Entities.Banners.GetById;

namespace Shop.Presentation.Facade.Entities.Banner;

public class BannerFacade : IBannerFacade
{
    private readonly IMediator _mediator;

    public BannerFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult<long>> Create(CreateBannerCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditBannerCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Remove(long id)
    {
        return await _mediator.Send(new RemoveBannerCommand(id));
    }

    public async Task<BannerDto?> GetById(long id)
    {
        return await _mediator.Send(new GetBannerByIdQuery(id));
    }

    public async Task<List<BannerDto>> GetAll()
    {
        return await _mediator.Send(new GetBannersListQuery());
    }
}