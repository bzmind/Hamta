namespace Shop.Domain.SellerAggregate.Services;

public interface ISellerDomainService
{
    bool IsSellerValid(Seller seller);
    bool IsDuplicateNationalCode(string nationalCode);
}