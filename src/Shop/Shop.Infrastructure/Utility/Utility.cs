using System.Globalization;
using Shop.Domain.CategoryAggregate;

namespace Shop.Infrastructure.Utility;

public static class Utility
{
    public static void FillSubCategories(this Category categoryToFill, List<Category> categoriesToFillFrom)
    {
        categoriesToFillFrom.ForEach(category =>
        {
            if (category.ParentId == categoryToFill.Id)
            {
                categoryToFill.AddSubCategory(category);
                FillSubCategories(category, categoriesToFillFrom);
            }
        });
    }
}