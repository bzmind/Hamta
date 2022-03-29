namespace Domain.Product_Aggregate.Services;

public interface IProductDomainService
{
    bool IsDuplicateSlug(string slug);
}