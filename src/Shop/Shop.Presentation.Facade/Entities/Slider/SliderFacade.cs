using Common.Application;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shop.Application.Entities.Sliders.Create;
using Shop.Application.Entities.Sliders.Delete;
using Shop.Application.Entities.Sliders.Edit;
using Shop.Presentation.Facade.Caching;
using Shop.Query.Entities._DTOs;
using Shop.Query.Entities.Sliders.GetAll;
using Shop.Query.Entities.Sliders.GetById;

namespace Shop.Presentation.Facade.Entities.Slider;

public class SliderFacade : ISliderFacade
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;

    public SliderFacade(IMediator mediator, IDistributedCache cache)
    {
        _mediator = mediator;
        _cache = cache;
    }

    public async Task<OperationResult<long>> Create(CreateSliderCommand command)
    {
        await _cache.RemoveAsync(CacheKeys.Sliders);
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditSliderCommand command)
    {
        await _cache.RemoveAsync(CacheKeys.Sliders);
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Remove(long sliderId)
    {
        await _cache.RemoveAsync(CacheKeys.Sliders);
        return await _mediator.Send(new RemoveSliderCommand(sliderId));
    }

    public async Task<SliderDto?> GetById(long id)
    {
        return await _mediator.Send(new GetSliderByIdQuery(id));

    }
    public async Task<List<SliderDto>> GetAll()
    {
        return await _cache.GetOrSet(CacheKeys.Sliders,
            async () => await _mediator.Send(new GetSlidersListQuery()));
    }
}