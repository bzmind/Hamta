﻿using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.ShippingAggregate;
using Shop.Domain.ShippingAggregate.Repository;

namespace Shop.Application.Shippings.Create;

public record CreateShippingCommand(string Name, int Cost) : IBaseCommand<long>;

public class CreateShippingCommandHandler : IBaseCommandHandler<CreateShippingCommand, long>
{
    private readonly IShippingRepository _shippingRepository;

    public CreateShippingCommandHandler(IShippingRepository shippingRepository)
    {
        _shippingRepository = shippingRepository;
    }

    public async Task<OperationResult<long>> Handle(CreateShippingCommand request, CancellationToken cancellationToken)
    {
        var shipping = new Shipping(request.Name, request.Cost);

        _shippingRepository.Add(shipping);
        await _shippingRepository.SaveAsync();
        return OperationResult<long>.Success(shipping.Id);
    }
}

public class CreateShippingCommandValidator : AbstractValidator<CreateShippingCommand>
{
    public CreateShippingCommandValidator()
    {
        RuleFor(s => s.Name)
            .NotNull().WithMessage(ValidationMessages.NameRequired)
            .NotEmpty().WithMessage(ValidationMessages.NameRequired)
            .MaximumLength(100).WithMessage(ValidationMessages.FieldCharactersMaxLength("نام", 100));

        RuleFor(s => s.Cost)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("هزینه"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("هزینه"))
            .GreaterThan(0).WithMessage(ValidationMessages.TomanMinAmount("هزینه", 0));
    }
}