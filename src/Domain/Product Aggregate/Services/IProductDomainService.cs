namespace Domain.Product_Aggregate.Services;

public interface IProductDomainService
{
    bool DoesSlugAlreadyExist(string slug);
}