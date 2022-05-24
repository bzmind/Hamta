﻿using FluentValidation;
using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using Shop.Domain.CommentAggregate;
using Shop.Domain.CommentAggregate.Repository;

namespace Shop.Application.Comments.Create;

public record CreateCommentCommand(long ProductId, long CustomerId, string Title, string Description,
    List<string> PositivePoints, List<string> NegativePoints, int RecommendationId) : IBaseCommand;

public class CreateCommentCommandHandler : IBaseCommandHandler<CreateCommentCommand>
{
    private readonly ICommentRepository _commentRepository;

    public CreateCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<OperationResult> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var recommendation = (Comment.CommentRecommendation) request.RecommendationId;

        var comment = new Comment(request.ProductId, request.CustomerId, request.Title, request.Description,
            recommendation);

        await _commentRepository.AddAsync(comment);

        if (request.PositivePoints.Count > 0)
            comment.SetPositivePoints(request.PositivePoints);

        if (request.NegativePoints.Count > 0)
            comment.SetNegativePoints(request.NegativePoints);

        _commentRepository.Add(comment);
        await _commentRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عنوان"));

        RuleFor(c => c.Description)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("توضیحات"));

        RuleFor(c => c.RecommendationId)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.Required);

        RuleForEach(c => c.NegativePoints)
            .MaximumLength(10);

        RuleForEach(c => c.PositivePoints)
            .MaximumLength(10);

        RuleForEach(c => c.PositivePoints).ChildRules(point =>
        {
            point.RuleFor(p => p)
                .NotNull()
                .MinimumLength(3).WithMessage(ValidationMessages.MinLength);
        });
        
        RuleForEach(c => c.NegativePoints).ChildRules(point =>
        {
            point.RuleFor(p => p)
                .NotNull()
                .MinimumLength(3).WithMessage(ValidationMessages.MinLength);
        });
    }
}