using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.CommentAggregate;
using Shop.Domain.CommentAggregate.Repository;

namespace Shop.Application.Comments.SetStatus;

public record SetCommentStatusCommand(long CommentId, string Status) : IBaseCommand;

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

        Enum.TryParse(request.Status, out Comment.CommentStatus status);
        comment.SetCommentStatus(status);

        await _commentRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class SetCommentStatusCommandValidator : AbstractValidator<SetCommentStatusCommand>
{
    public SetCommentStatusCommandValidator()
    {
        RuleFor(c => c.CommentId)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("آیدی"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("آیدی"));

        RuleFor(c => c.Status)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("وضعیت"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("وضعیت"))
            .IsEnumName(typeof(Comment.CommentStatus), false)
            .WithMessage(ValidationMessages.FieldInvalid("وضعیت"));
    }
}