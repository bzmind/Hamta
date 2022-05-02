using Shop.Domain.ProductAggregate.Repository;
using Shop.Domain.ProductAggregate.Services;

namespace Shop.Application.Products._Services;

public class ProductDomainService : IProductDomainService
{
    private readonly IProductRepository _productRepository;

    public ProductDomainService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public bool IsDuplicateSlug(string slug)
    {
        return _productRepository.Exists(c => c.Slug == slug);
    }
}