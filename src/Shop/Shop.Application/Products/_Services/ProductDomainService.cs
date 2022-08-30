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

    public bool IsDuplicateSlug(long id, string slug)
    {
        var product = _productRepository.GetProductBySlug(slug);
        if (product == null)
            return false;
        if (product.Id == id)
            return false;
        return true;
    }
}