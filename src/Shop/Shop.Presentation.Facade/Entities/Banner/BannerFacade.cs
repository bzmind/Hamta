using Common.Application;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shop.Application.Entities.Banners.Create;
using Shop.Application.Entities.Banners.Delete;
using Shop.Application.Entities.Banners.Edit;
using Shop.Presentation.Facade.Caching;
using Shop.Query.Entities._DTOs;
using Shop.Query.Entities.Banners.GetAll;
using Shop.Query.Entities.Banners.GetById;

namespace Shop.Presentation.Facade.Entities.Banner;

public class BannerFacade : IBannerFacade
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;

    public BannerFacade(IMediator mediator, IDistributedCache cache)
    {
        _mediator = mediator;
        _cache = cache;
    }

    public async Task<OperationResult<long>> Create(CreateBannerCommand command)
    {
        await _cache.RemoveAsync(CacheKeys.Banners);
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditBannerCommand command)
    {
        await _cache.RemoveAsync(CacheKeys.Banners);
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Remove(long id)
    {
        await _cache.RemoveAsync(CacheKeys.Banners);
        return await _mediator.Send(new RemoveBannerCommand(id));
    }

    public async Task<BannerDto?> GetById(long id)
    {
        return await _mediator.Send(new GetBannerByIdQuery(id));
    }

    public async Task<List<BannerDto>> GetAll()
    {
        return await _cache.GetOrSet(CacheKeys.Banners,
            async () => await _mediator.Send(new GetBannersListQuery()));
    }
}