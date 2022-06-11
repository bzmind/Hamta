using FluentValidation;
using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using Shop.Domain.CommentAggregate;
using Shop.Domain.CommentAggregate.Repository;

namespace Shop.Application.Comments.Create;

public class CreateCommentCommand : IBaseCommand<long>
{
    public long ProductId { get; set; }
    public long UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<string> PositivePoints { get; set; }
    public List<string> NegativePoints { get; set; }
    public string Recommendation { get; set; }

    public CreateCommentCommand(long productId, long userId, string title, string description,
        List<string> positivePoints, List<string> negativePoints, string recommendation)
    {
        ProductId = productId;
        UserId = userId;
        Title = title;
        Description = description;
        PositivePoints = positivePoints;
        NegativePoints = negativePoints;
        Recommendation = recommendation;
    }
}

public class CreateCommentCommandHandler : IBaseCommandHandler<CreateCommentCommand, long>
{
    private readonly ICommentRepository _commentRepository;

    public CreateCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<OperationResult<long>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        Enum.TryParse(request.Recommendation, out Comment.CommentRecommendation recommendation);

        var comment = new Comment(request.ProductId, request.UserId, request.Title, request.Description,
            recommendation);

        await _commentRepository.AddAsync(comment);

        if (request.PositivePoints.Count > 0)
            comment.SetPositivePoints(request.PositivePoints);

        if (request.NegativePoints.Count > 0)
            comment.SetNegativePoints(request.NegativePoints);

        _commentRepository.Add(comment);
        await _commentRepository.SaveAsync();
        return OperationResult<long>.Success(comment.Id);
    }
}

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("عنوان"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عنوان"));

        RuleFor(c => c.Description)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("توضیحات"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("توضیحات"));

        RuleFor(c => c.Recommendation)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("نوع پیشنهاد"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("نوع پیشنهاد"))
            .IsEnumName(typeof(Comment.CommentRecommendation), false)
            .WithMessage(ValidationMessages.FieldInvalid("نوع پیشنهاد"));

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