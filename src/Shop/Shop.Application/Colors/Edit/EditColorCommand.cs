using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.ColorAggregate.Repository;

namespace Shop.Application.Colors.Edit;

public record EditColorCommand(long ColorId, string Name, string Code) : IBaseCommand;

public class EditColorCommandHandler : IBaseCommandHandler<EditColorCommand>
{
    private readonly IColorRepository _colorRepository;

    public EditColorCommandHandler(IColorRepository colorRepository)
    {
        _colorRepository = colorRepository;
    }

    public async Task<OperationResult> Handle(EditColorCommand request, CancellationToken cancellationToken)
    {
        var color = await _colorRepository.GetAsTrackingAsync(request.ColorId);

        if (color == null)
            return OperationResult.NotFound();

        color.Edit(request.Name, request.Code);

        await _colorRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class EditColorCommandValidator : AbstractValidator<EditColorCommand>
{
    public EditColorCommandValidator()
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