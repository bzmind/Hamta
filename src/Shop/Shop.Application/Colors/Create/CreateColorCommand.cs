using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.ColorAggregate;
using Shop.Domain.ColorAggregate.Repository;

namespace Shop.Application.Colors.Create;

public record CreateColorCommand(string Name, string Code) : IBaseCommand<long>;

public class AddColorCommandHandler : IBaseCommandHandler<CreateColorCommand, long>
{
    private readonly IColorRepository _colorRepository;

    public AddColorCommandHandler(IColorRepository colorRepository)
    {
        _colorRepository = colorRepository;
    }

    public async Task<OperationResult<long>> Handle(CreateColorCommand request, CancellationToken cancellationToken)
    {
        var color = new Color(request.Name, request.Code);
        _colorRepository.Add(color);
        await _colorRepository.SaveAsync();
        return OperationResult<long>.Success(color.Id);
    }
}

public class AddColorCommandValidator : AbstractValidator<CreateColorCommand>
{
    public AddColorCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotNull().WithMessage(ValidationMessages.ColorNameRequired)
            .NotEmpty().WithMessage(ValidationMessages.ColorNameRequired)
            .MaximumLength(50).WithMessage(ValidationMessages.FieldCharactersMaxLength("نام رنگ", 50));

        RuleFor(c => c.Code)
            .NotNull().WithMessage(ValidationMessages.ColorCodeRequired)
            .NotEmpty().WithMessage(ValidationMessages.ColorCodeRequired)
            .MaximumLength(7).WithMessage(ValidationMessages.FieldCharactersMaxLength("کد رنگ", 7));
    }
}