using Shop.Domain.SellerAggregate;
using Shop.Domain.SellerAggregate.Repository;
using Shop.Domain.SellerAggregate.Services;

namespace Shop.Application.Sellers._Services;

public class SellerDomainService : ISellerDomainService
{
    private readonly ISellerRepository _repository;

    public SellerDomainService(ISellerRepository repository)
    {
        _repository = repository;
    }

    public bool IsSellerValid(Seller seller)
    {
        var sellerIsExist = _repository.Exists(s => s.NationalCode == seller.NationalCode ||
                                                    s.UserId == seller.UserId);
        return !sellerIsExist;
    }

    public bool IsDuplicateNationalCode(string nationalCode)
    {
        return _repository.Exists(r => r.NationalCode == nationalCode);
    }
}