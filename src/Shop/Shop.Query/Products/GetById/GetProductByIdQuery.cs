using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.CategoryAggregate.Repository;
using Shop.Infrastructure;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Products._DTOs;
using Shop.Query.Products._Mappers;

namespace Shop.Query.Products.GetById;

public record GetProductByIdQuery(long ProductId) : IBaseQuery<ProductDto?>;

public class GetProductByIdQueryHandler : IBaseQueryHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly ShopContext _shopContext;
    private readonly DapperContext _dapperContext;
    private readonly ICategoryRepository _categoryRepository;

    public GetProductByIdQueryHandler(ShopContext shopContext, ICategoryRepository categoryRepository,
        DapperContext dapperContext)
    {
        _shopContext = shopContext;
        _categoryRepository = categoryRepository;
        _dapperContext = dapperContext;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var productDtos = await _shopContext.Products
                .Where(product => product.Id == request.ProductId)
                .GroupJoin(
                    _shopContext.Categories,
                    product => product.CategoryId,
                    category => category.Id,
                    (product, categories) => new
                    { Product = product, Categories = categories })
                .SelectMany(
                    tables => tables.Categories.DefaultIfEmpty(),
                    (tables, category) => new
                    { tables.Product, Category = category })
                .ToListAsync(cancellationToken);

        var productDto = productDtos
            .Select(t => t.Product.MapToProductDto())
            .GroupBy(product => product.Id).Select(grouping =>
            {
                var firstItem = grouping.First();
                firstItem.GalleryImages = grouping
                    .Select(p => p.GalleryImages.OrderBy(gi => gi.Sequence).ToList()).First();
                firstItem.Specifications = grouping.Select(p => p.Specifications).First();
                firstItem.CategorySpecifications = grouping.Select(p => p.CategorySpecifications).First();
                return firstItem;
            }).Single();

        var specs = await _categoryRepository.GetCategoryAndParentsSpecifications(productDto.CategoryId);
        productDto.CategorySpecifications = specs
            .MapToProductCategorySpecificationQueryDto(productDto.CategorySpecifications);

        return productDto;
    }
}