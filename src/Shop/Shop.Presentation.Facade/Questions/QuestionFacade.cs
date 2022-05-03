using Common.Application;
using MediatR;
using Shop.Application.Questions.Create;
using Shop.Application.Questions.SetStatus;
using Shop.Query.Questions._DTOs;
using Shop.Query.Questions.GetByFilter;
using Shop.Query.Questions.GetById;

namespace Shop.Presentation.Facade.Questions;

internal class QuestionFacade : IQuestionFacade
{
    private readonly IMediator _mediator;

    public QuestionFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> Create(CreateQuestionCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> SetStatus(SetQuestionStatusCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<QuestionDto?> GetQuestionById(long id)
    {
        return await _mediator.Send(new GetQuestionByIdQuery(id));
    }

    public async Task<QuestionFilterResult> GetQuestionByFilter(QuestionFilterParam filterParams)
    {
        return await _mediator.Send(new GetQuestionByFilterQuery(filterParams));
    }
}