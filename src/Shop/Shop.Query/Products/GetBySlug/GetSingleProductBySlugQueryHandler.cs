using Common.Query.BaseClasses;
using Dapper;
using Shop.Domain.CategoryAggregate.Repository;
using Shop.Infrastructure;
using Shop.Query.Categories._DTOs;
using Shop.Query.Products._DTOs;
using Shop.Query.Products._Mappers;

namespace Shop.Query.Products.GetBySlug;

public record GetSingleProductBySlugQuery(string Slug) : IBaseQuery<SingleProductDto?>;

public class GetSingleProductBySlugQueryHandler : IBaseQueryHandler<GetSingleProductBySlugQuery, SingleProductDto?>
{
    private readonly DapperContext _dapperContext;
    private readonly ICategoryRepository _categoryRepository;

    public GetSingleProductBySlugQueryHandler(DapperContext dapperContext, ICategoryRepository categoryRepository)
    {
        _dapperContext = dapperContext;
        _categoryRepository = categoryRepository;
    }

    public async Task<SingleProductDto?> Handle(GetSingleProductBySlugQuery request, CancellationToken cancellationToken)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $@"
            SELECT DISTINCT
                p.Id, p.CreationDate, p.CategoryId, p.Name, p.EnglishName, p.Slug, p.Introduction,
                p.Review, p.MainImage, AVG(cmnt.Score) OVER (PARTITION BY p.Id) AS AverageScore,
                pg.*, pspec.*, pcs.*,
                si.Id, si.CreationDate, si.SellerId, si.ProductId, s.ShopName,
                clr.Id AS ColorID, clr.Name AS ColorName, clr.Code AS ColorCode,
                si.Quantity, si.Price, si.IsAvailable, si.DiscountPercentage, si.IsDiscounted
            FROM product.Products p
            LEFT JOIN {_dapperContext.Comments} cmnt
            	ON cmnt.ProductId = p.Id
            LEFT JOIN {_dapperContext.ProductGalleryImages} pg
            	ON pg.ProductId = p.Id
            LEFT JOIN {_dapperContext.ProductSpecifications} pspec
            	ON pspec.ProductId = p.Id
            LEFT JOIN {_dapperContext.ProductCategorySpecifications} pcs
            	ON pcs.ProductId = p.Id
            LEFT JOIN {_dapperContext.Categories} c
            	ON p.CategoryId = c.Id
            LEFT JOIN {_dapperContext.SellerInventories} si
            	ON si.ProductId = p.Id
            LEFT JOIN {_dapperContext.Colors} clr
            	ON clr.Id = si.ColorId
            LEFT JOIN {_dapperContext.Sellers} s
            	ON s.Id = si.SellerId
            WHERE p.Slug = @Slug";

        var singleProductDtos = await connection.QueryAsync<SingleProductDto, ProductGalleryImageDto,
        ProductSpecificationQueryDto, ProductCategorySpecificationQueryDto, ProductInventoryDto, SingleProductDto>
        (sql, (singleProduct, galleryImage, spec, categorySpec, inventory) =>
        {
            if (galleryImage != null)
                singleProduct.GalleryImages.Add(galleryImage);
            if (spec != null)
                singleProduct.Specifications.Add(spec);
            if (categorySpec != null)
                singleProduct.CategorySpecifications.Add(categorySpec);
            if (inventory != null)
                singleProduct.Inventories.Add(inventory);
            return singleProduct;
        }, param: new { request.Slug }, splitOn: "Id,Id,Id,Id");

        var singleProductDto = singleProductDtos.GroupBy(p => p.Id).Select(group =>
        {
            var product = group.First();

            if (group.Select(p => p.GalleryImages).Any(list => list.Any()))
                product.GalleryImages = group.Select(p => p.GalleryImages.First()).DistinctBy(p => p.Id).ToList();

            if (group.Select(p => p.Specifications).Any(list => list.Any()))
                product.Specifications = group.Select(p => p.Specifications.First()).DistinctBy(p => p.Id).ToList();

            if (group.Select(p => p.CategorySpecifications).Any(list => list.Any()))
                product.CategorySpecifications = group.Select(p => p.CategorySpecifications.First())
                    .DistinctBy(p => p.Id).ToList();

            if (group.Select(p => p.Inventories).Any(list => list.Any()))
                product.Inventories = group.Select(p => p.Inventories.First()).DistinctBy(p => p.Id).ToList();

            return product;
        }).First();
        
        using var categoryConnection = _dapperContext.CreateConnection();
        var categorySql = $@"
            WITH parent AS (
               SELECT *
               FROM {_dapperContext.Categories}
               WHERE Id = @CategoryId
               UNION ALL
               SELECT c.*
               FROM {_dapperContext.Categories} c
               JOIN parent p
            		ON p.ParentId = c.Id
            ) 
            SELECT *
            FROM parent p
            ORDER BY p.Id ASC";

        var categoryDtos = await categoryConnection.QueryAsync<CategoryDto>(categorySql,
            new { singleProductDto.CategoryId });

        var categoryDto = categoryDtos.ToList().First(c => c.ParentId == null);
        FillSubCategories(categoryDto, categoryDtos.ToList());
        singleProductDto.Category = categoryDto;

        var categorySpecs = await _categoryRepository.GetCategoryAndParentsSpecifications(singleProductDto.CategoryId);
        singleProductDto.CategorySpecifications = categorySpecs
            .MapToProductCategorySpecificationQueryDto(singleProductDto.CategorySpecifications);

        return singleProductDto;
    }

    private void FillSubCategories(CategoryDto categoryToFill, List<CategoryDto> categoriesToFillFrom)
    {
        categoriesToFillFrom.ForEach(category =>
        {
            if (category.ParentId == categoryToFill.Id)
            {
                categoryToFill.SubCategories.Add(category);
                FillSubCategories(category, categoriesToFillFrom);
            }
        });
    }
}