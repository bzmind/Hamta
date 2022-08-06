using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.SellerAggregate;
using Shop.Domain.SellerAggregate.Repository;
using Shop.Domain.SellerAggregate.Services;

namespace Shop.Application.Sellers.Create;

public class CreateSellerCommand : IBaseCommand<long>
{
    public long UserId { get; set; }
    public string ShopName { get; set; }
    public string NationalCode { get; set; }
}

public class CreateSellerCommandHandler : IBaseCommandHandler<CreateSellerCommand, long>
{
    private readonly ISellerRepository _sellerRepository;
    private readonly ISellerDomainService _sellerDomainService;

    public CreateSellerCommandHandler(ISellerRepository sellerRepository, ISellerDomainService sellerDomainService)
    {
        _sellerRepository = sellerRepository;
        _sellerDomainService = sellerDomainService;
    }

    public async Task<OperationResult<long>> Handle(CreateSellerCommand request, CancellationToken cancellationToken)
    {
        var seller = new Seller(request.UserId, request.ShopName, request.NationalCode, _sellerDomainService);

        _sellerRepository.Add(seller);
        await _sellerRepository.SaveAsync();
        return OperationResult<long>.Success(seller.Id);
    }
}

public class CreateSellerCommandValidator : AbstractValidator<CreateSellerCommand>
{
    public CreateSellerCommandValidator()
    {
        RuleFor(i => i.ShopName)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("نام فروشگاه"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("نام فروشگاه"))
            .MaximumLength(50).WithMessage(ValidationMessages.FieldCharactersMaxLength("عنوان", 50));

        RuleFor(i => i.NationalCode)
            .NotNull().WithMessage(ValidationMessages.NationalCodeRequired)
            .NotEmpty().WithMessage(ValidationMessages.NationalCodeRequired)
            .Length(10).WithMessage(ValidationMessages.FieldCharactersStaticLength("کدملی", 10));
    }
}