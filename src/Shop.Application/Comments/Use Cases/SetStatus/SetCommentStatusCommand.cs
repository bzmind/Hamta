using Common.Application.Utility;
using Common.Domain.Exceptions;
using FluentValidation;
using MediatR;
using Shop.Domain.Comment_Aggregate;
using Shop.Domain.Comment_Aggregate.Repository;

namespace Shop.Application.Comments.Use_Cases.SetStatus;

public record SetCommentStatusCommand(long CommentId, Comment.CommentStatus Status) : IRequest;

public class SetCommentStatusCommandHandler : IRequestHandler<SetCommentStatusCommand>
{
    private readonly ICommentRepository _commentRepository;

    public SetCommentStatusCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Unit> Handle(SetCommentStatusCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetAsTrackingAsync(request.CommentId);

        if (comment == null)
            throw new DataNotFoundInDatabaseDomainException("No comment was found with the passed ID");

        comment.SetCommentStatus(request.Status);
        await _commentRepository.SaveAsync();
        return Unit.Value;
    }
}

internal class SetCommentStatusCommandValidator : AbstractValidator<SetCommentStatusCommand>
{
    public SetCommentStatusCommandValidator()
    {
        RuleFor(c => c.Status)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.Required);
    }
}