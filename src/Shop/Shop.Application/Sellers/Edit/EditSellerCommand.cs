using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Application.Sellers.Create;
using Shop.Domain.SellerAggregate.Repository;
using Shop.Domain.SellerAggregate.Services;

namespace Shop.Application.Sellers.Edit;

public class EditSellerCommand : IBaseCommand
{
    public long UserId { get; set; }
    public string ShopName { get; set; }
    public string NationalCode { get; set; }
}

public class EditSellerCommandHandler : IBaseCommandHandler<EditSellerCommand>
{
    private readonly ISellerRepository _repository;
    private readonly ISellerDomainService _domainService;
    public EditSellerCommandHandler(ISellerRepository repository, ISellerDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }

    public async Task<OperationResult> Handle(EditSellerCommand request, CancellationToken cancellationToken)
    {
        var seller = await _repository.GetSellerByUserIdAsTrackingAsync(request.UserId);
        if (seller == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("فروشنده"));

        seller.Edit(request.ShopName, request.NationalCode, _domainService);
        await _repository.SaveAsync();
        return OperationResult.Success();
    }
}

public class EditSellerCommandValidator : AbstractValidator<CreateSellerCommand>
{
    public EditSellerCommandValidator()
    {
        RuleFor(i => i.ShopName)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("نام فروشگاه"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("نام فروشگاه"))
            .MaximumLength(50).WithMessage(ValidationMessages.FieldCharactersMaxLength("عنوان", 50));

        RuleFor(i => i.NationalCode)
            .NotNull().WithMessage(ValidationMessages.NationalCodeRequired)
            .NotEmpty().WithMessage(ValidationMessages.NationalCodeRequired)
            .MaximumLength(10).WithMessage(ValidationMessages.FieldCharactersStaticLength("کدملی", 10));
    }
}