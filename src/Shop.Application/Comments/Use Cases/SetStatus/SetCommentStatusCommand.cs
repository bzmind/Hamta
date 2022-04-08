using Common.Application;
using Common.Application.Base_Classes;
using Common.Application.Utility;
using FluentValidation;
using Shop.Domain.Comment_Aggregate;
using Shop.Domain.Comment_Aggregate.Repository;

namespace Shop.Application.Comments.Use_Cases.SetStatus;

public record SetCommentStatusCommand(long CommentId, Comment.CommentStatus Status) : IBaseCommand;

public class SetCommentStatusCommandHandler : IBaseCommandHandler<SetCommentStatusCommand>
{
    private readonly ICommentRepository _commentRepository;

    public SetCommentStatusCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<OperationResult> Handle(SetCommentStatusCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetAsTrackingAsync(request.CommentId);

        if (comment == null)
            return OperationResult.NotFound();

        comment.SetCommentStatus(request.Status);
        await _commentRepository.SaveAsync();
        return OperationResult.Success();
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