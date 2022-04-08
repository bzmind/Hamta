using Common.Application;
using Common.Application.Base_Classes;
using Common.Application.Utility;
using FluentValidation;
using Shop.Domain.Comment_Aggregate;
using Shop.Domain.Comment_Aggregate.Repository;

namespace Shop.Application.Comments.Use_Cases.Create;

public record CreateCommentCommand(long ProductId, long CustomerId, string Title, string Description,
    List<string> PositivePoints, List<string> NegativePoints,
    Comment.CommentRecommendation Recommendation) : IBaseCommand;

public class CreateCommentCommandHandler : IBaseCommandHandler<CreateCommentCommand>
{
    private readonly ICommentRepository _commentRepository;

    public CreateCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<OperationResult> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = new Comment(request.ProductId, request.CustomerId, request.Title, request.Description,
            request.Recommendation);

        if (request.PositivePoints.Count > 0)
        {
            var positivePoints = new List<string>();
            request.PositivePoints.ForEach(point => positivePoints.Add(point));
            comment.SetPositivePoints(positivePoints);
        }

        if (request.NegativePoints.Count > 0)
        {
            var negativePoints = new List<string>();
            request.NegativePoints.ForEach(point => negativePoints.Add(point));
            comment.SetNegativePoints(negativePoints);
        }

        await _commentRepository.AddAsync(comment);
        await _commentRepository.SaveAsync();
        return OperationResult.Success();
    }
}

internal class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عنوان"));

        RuleFor(c => c.Description)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("توضیحات"));

        RuleFor(c => c.Recommendation)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.Required);

        RuleForEach(c => c.PositivePoints).ChildRules(point =>
        {
            point.RuleFor(p => p)
                .NotNull()
                .MinimumLength(1).WithMessage(ValidationMessages.MinLength);
        });

        RuleForEach(c => c.NegativePoints).ChildRules(point =>
        {
            point.RuleFor(p => p)
                .NotNull()
                .MinimumLength(1).WithMessage(ValidationMessages.MinLength);
        });
    }
}