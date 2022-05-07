using Microsoft.EntityFrameworkCore;
using Shop.Domain.CategoryAggregate;
using Shop.Domain.CategoryAggregate.Repository;
using Shop.Infrastructure.BaseClasses;
using Shop.Infrastructure.Utility;

namespace Shop.Infrastructure.Persistence.EF.Categories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ShopContext context) : base(context)
    {
    }

    public async Task<bool> RemoveCategory(long categoryId)
    {
        var categories = await Context.Categories
            .OrderBy(c => c.Id)
            .Where(c => c.Id >= categoryId)
            .GroupJoin(
                Context.Products,
                c => c.Id,
                p => p.CategoryId,
                (category, product) => new
                {
                    category,
                    product
                })
            .SelectMany(
                t => t.product.DefaultIfEmpty(),
                (c, p) => new { c.category, product = p })
            .ToListAsync();

        var exactCategory = categories.Select(t => t.category).FirstOrDefault();

        if (exactCategory == null || exactCategory.Id != categoryId)
            return false;

        var allCategoriesGrouped = categories
            .GroupBy(tables => tables.category.Id)
            .Select(groupedCategory =>
            {
                var firstItem = groupedCategory.Select(t => t.category).First();
                return firstItem;
            });

        exactCategory.FillSubCategories(allCategoriesGrouped.ToList());

        var categoriesWithProduct = categories
            .Where(t => t.product != null)
            .GroupBy(tables => tables.category.Id)
            .Select(groupedCategory =>
            {
                var firstItem = groupedCategory.Select(t => t.category).First();
                return firstItem;
            });

        if (CategoryHasProduct(exactCategory, categoriesWithProduct.ToList()))
            return false;

        Context.Categories.RemoveRange(exactCategory.SubCategories);
        Context.Categories.Remove(exactCategory);

        return true;
    }

    private bool CategoryHasProduct(Category category, List<Category> categoriesWithProduct)
    {
        bool isThereCategoryWithProduct = false;

        if (categoriesWithProduct.Any(c => c.Id == category.Id))
            return true;

        category.SubCategories.ToList().ForEach(subCategory =>
        {
            if (categoriesWithProduct.Any(c => c.Id == subCategory.Id))
            {
                isThereCategoryWithProduct = true;
                return;
            }

            isThereCategoryWithProduct = CategoryHasProduct(subCategory, categoriesWithProduct);
        });

        if (isThereCategoryWithProduct)
            return true;

        return false;
    }
}