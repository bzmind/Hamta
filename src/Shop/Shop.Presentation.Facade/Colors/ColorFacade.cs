﻿using Common.Application;
using MediatR;
using Shop.Application.Colors.Create;
using Shop.Application.Colors.Edit;
using Shop.Query.Colors._DTOs;
using Shop.Query.Colors.GetByFilter;
using Shop.Query.Colors.GetById;

namespace Shop.Presentation.Facade.Colors;

internal class ColorFacade : IColorFacade
{
    private readonly IMediator _mediator;

    public ColorFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult<long>> Create(CreateColorCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditColorCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<ColorDto?> GetById(long id)
    {
        return await _mediator.Send(new GetColorByIdQuery(id));
    }

    public async Task<ColorFilterResult> GetByFilter(ColorFilterParams filterParams)
    {
        return await _mediator.Send(new GetColorByFilterQuery(filterParams));
    }
}