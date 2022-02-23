namespace Domain.Category_Aggregate.Services;

public interface ICategoryService
{
    bool DoesCategoryExist(long categoryId);
}