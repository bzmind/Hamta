﻿using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.CommentAggregate;
using Shop.Domain.CommentAggregate.Repository;

namespace Shop.Application.Comments.UseCases.SetStatus;

public record SetCommentStatusCommand(long CommentId, int StatusId) : IBaseCommand;

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

        var status = (Comment.CommentStatus) request.StatusId;
        comment.SetCommentStatus(status);

        await _commentRepository.SaveAsync();
        return OperationResult.Success();
    }
}

internal class SetCommentStatusCommandValidator : AbstractValidator<SetCommentStatusCommand>
{
    public SetCommentStatusCommandValidator()
    {
        RuleFor(c => c.StatusId)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.Required);
    }
}