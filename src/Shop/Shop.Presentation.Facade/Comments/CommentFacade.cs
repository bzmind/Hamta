using Common.Application;
using MediatR;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Remove;
using Shop.Application.Comments.SetDislikes;
using Shop.Application.Comments.SetLikes;
using Shop.Application.Comments.SetStatus;
using Shop.Query.Comments._DTOs;
using Shop.Query.Comments.GetByFilter;
using Shop.Query.Comments.GetById;

namespace Shop.Presentation.Facade.Comments;

internal class CommentFacade : ICommentFacade
{
    private readonly IMediator _mediator;

    public CommentFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult<long>> Create(CreateCommentCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> SetStatus(SetCommentStatusCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> SetLikes(SetCommentLikesCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> SetDislikes(SetCommentDislikesCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Remove(long commentId)
    {
        return await _mediator.Send(new RemoveCommentCommand(commentId));
    }

    public async Task<CommentDto?> GetById(long id)
    {
        return await _mediator.Send(new GetCommentByIdQuery(id));
    }

    public async Task<CommentFilterResult> GetByFilter(CommentFilterParams filterParams)
    {
        return await _mediator.Send(new GetCommentByFilterQuery(filterParams));
    }
}