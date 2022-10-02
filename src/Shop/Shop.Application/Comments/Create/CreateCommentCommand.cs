using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.CommentAggregate;
using Shop.Domain.CommentAggregate.Repository;

namespace Shop.Application.Comments.Create;

public class CreateCommentCommand : IBaseCommand<long>
{
    public long UserId { get; set; }
    public long ProductId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Score { get; set; }
    public List<string>? PositivePoints { get; set; }
    public List<string>? NegativePoints { get; set; }
    public Comment.CommentRecommendation Recommendation { get; set; }

    private CreateCommentCommand()
    {

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
        var comment = new Comment(request.ProductId, request.UserId, request.Title, request.Description,
            request.Score, request.Recommendation);

        await _commentRepository.AddAsync(comment);

        if (request.PositivePoints != null && request.PositivePoints.Any())
            comment.SetPositivePoints(request.PositivePoints);

        if (request.NegativePoints != null && request.NegativePoints.Any())
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
        RuleFor(r => r.Title)
            .NotNull().WithMessage(ValidationMessages.TitleRequired)
            .NotEmpty().WithMessage(ValidationMessages.TitleRequired)
            .MaximumLength(100).WithMessage(ValidationMessages.FieldCharactersMaxLength("عنوان", 100));

        RuleFor(r => r.Description)
            .NotNull().WithMessage(ValidationMessages.DescriptionRequired)
            .NotEmpty().WithMessage(ValidationMessages.DescriptionRequired)
            .MaximumLength(1500).WithMessage(ValidationMessages.FieldCharactersMaxLength("توضیحات", 1500));

        RuleFor(r => r.Recommendation)
            .NotNull().WithMessage(ValidationMessages.CommentRecommendationRequired)
            .IsInEnum().WithMessage(ValidationMessages.InvalidCommentRecommendation);

        RuleForEach(r => r.NegativePoints)
            .NotNull().WithMessage(ValidationMessages.MinCharactersLength)
            .MinimumLength(3).WithMessage(ValidationMessages.FieldCharactersMinLength("متن", 3));

        RuleForEach(r => r.PositivePoints)
            .NotNull().WithMessage(ValidationMessages.MinCharactersLength)
            .MinimumLength(3).WithMessage(ValidationMessages.FieldCharactersMinLength("متن", 3));
    }
}