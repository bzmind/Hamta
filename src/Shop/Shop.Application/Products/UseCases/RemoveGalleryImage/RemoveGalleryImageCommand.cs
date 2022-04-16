using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.ProductAggregate.Repository;

namespace Shop.Application.Products.UseCases.RemoveGalleryImage;

public record RemoveGalleryImageCommand(long ProductId, long GalleryImageId) : IBaseCommand;

public class RemoveGalleryImageCommandHandler : IBaseCommandHandler<RemoveGalleryImageCommand>
{
    private readonly IProductRepository _productRepository;

    public RemoveGalleryImageCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<OperationResult> Handle(RemoveGalleryImageCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsTrackingAsync(request.ProductId);

        if (product == null)
            return OperationResult.NotFound();

        product.RemoveGalleryImage(request.GalleryImageId);

        await _productRepository.SaveAsync();
        return OperationResult.Success();
    }
}