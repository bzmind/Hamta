namespace Domain.Category_Aggregate.Services;

public interface ICategoryDomainService
{
    bool DoesCategoryExist(long categoryId);
    bool DoesSlugAlreadyExist(string slug);
}