namespace Shop.Domain.ProductAggregate.Services;

public interface IProductDomainService
{
    bool IsDuplicateSlug(string slug);
}