using Dapper;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.CategoryAggregate;
using Shop.Domain.CategoryAggregate.Repository;
using Shop.Infrastructure.BaseClasses;
using Shop.Infrastructure.Utility;

namespace Shop.Infrastructure.Persistence.EF.Categories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    private readonly DapperContext _dapperContext;

    public CategoryRepository(ShopContext context, DapperContext dapperContext) : base(context)
    {
        _dapperContext = dapperContext;
    }

    public Category? GetCategoryBySlug(string slug)
    {
        return Context.Categories.FirstOrDefault(category => category.Slug == slug);
    }

    public async Task<List<CategorySpecification>> GetCategoryAndParentsSpecifications(long categoryId)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $@"WITH parent_specs AS (
                    	SELECT c.ParentId, cs.*
                    	FROM {_dapperContext.Categories} c
                    	LEFT JOIN {_dapperContext.CategorySpecifications} cs
                    		ON cs.CategoryId = c.Id
                    	WHERE c.Id = @categoryId
                    
                    	UNION ALL
                    
                    	SELECT c.ParentId, cs.*
                    	FROM {_dapperContext.Categories} c
                    	JOIN parent_specs p
                    		ON p.ParentId = c.Id
                    	JOIN {_dapperContext.CategorySpecifications} cs
                    		ON cs.CategoryId = c.Id
                    )
                    
                    SELECT *
                    INTO #TempTable
                    FROM parent_specs
                    ALTER TABLE #TempTable DROP COLUMN ParentId
                    SELECT * FROM #TempTable tmp
                    WHERE tmp.Id IS NOT NULL
                    ORDER BY tmp.Id ASC
                    DROP TABLE #TempTable";

        var result = await connection.QueryAsync<CategorySpecification>(sql, new { categoryId });
        return result.ToList();
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
                (category, product) => new { category, product })
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

        RecursivelyRemoveSubCategories(exactCategory, Context);
        Context.Categories.Remove(exactCategory);

        return true;
    }

    private void RecursivelyRemoveSubCategories(Category category, ShopContext context)
    {
        if (category.SubCategories.Any())
        {
            foreach (var subCategory in category.SubCategories)
            {
                RecursivelyRemoveSubCategories(subCategory, context);
            }
        }
        context.Categories.RemoveRange(category);
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