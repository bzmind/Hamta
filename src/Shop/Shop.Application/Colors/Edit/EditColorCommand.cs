using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
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
            .NotNull().WithMessage(ValidationMessages.FieldRequired("نام رنگ"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("نام رنگ"));

        RuleFor(c => c.Code)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("کد رنگ"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("کد رنگ"));
    }
}