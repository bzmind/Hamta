namespace Shop.Domain.ProductAggregate.Services;

public interface IProductDomainService
{
    bool IsDuplicateSlug(long id, string slug);
}