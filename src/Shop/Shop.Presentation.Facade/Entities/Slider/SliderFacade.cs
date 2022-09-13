using Common.Application;
using MediatR;
using Shop.Application.Entities.Sliders.Create;
using Shop.Application.Entities.Sliders.Delete;
using Shop.Application.Entities.Sliders.Edit;
using Shop.Query.Entities._DTOs;
using Shop.Query.Entities.Sliders.GetAll;
using Shop.Query.Entities.Sliders.GetById;

namespace Shop.Presentation.Facade.Entities.Slider;

public class SliderFacade : ISliderFacade
{
    private readonly IMediator _mediator;

    public SliderFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult<long>> Create(CreateSliderCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditSliderCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Remove(long sliderId)
    {
        return await _mediator.Send(new RemoveSliderCommand(sliderId));
    }

    public async Task<SliderDto?> GetById(long id)
    {
        return await _mediator.Send(new GetSliderByIdQuery(id));

    }
    public async Task<List<SliderDto>> GetAll()
    {
        return await _mediator.Send(new GetSlidersListQuery());
    }
}