using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.ColorAggregate;
using Shop.Domain.ColorAggregate.Repository;

namespace Shop.Application.Colors.UseCases.Create;

public record CreateColorCommand(string Name, string Code) : IBaseCommand;

public class AddColorCommandHandler : IBaseCommandHandler<CreateColorCommand>
{
    private readonly IColorRepository _colorRepository;

    public AddColorCommandHandler(IColorRepository colorRepository)
    {
        _colorRepository = colorRepository;
    }

    public async Task<OperationResult> Handle(CreateColorCommand request, CancellationToken cancellationToken)
    {
        var color = new Color(request.Name, request.Code);

        await _colorRepository.AddAsync(color);
        await _colorRepository.SaveAsync();
        return OperationResult.Success();
    }
}

internal class AddColorCommandValidator : AbstractValidator<CreateColorCommand>
{
    public AddColorCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("نام رنگ"));

        RuleFor(c => c.Code)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("کد رنگ"));
    }
}