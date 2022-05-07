using Shop.Domain.CategoryAggregate;

namespace Shop.Infrastructure.Utility;

public static class Utility
{
    public static void FillSubCategories(this Category parentToBeFilled, List<Category> categories)
    {
        categories.ForEach(category =>
        {
            if (category.ParentId == parentToBeFilled.Id)
            {
                parentToBeFilled.AddSubCategory(category);
                FillSubCategories(category, categories);
            }
        });
    }
}