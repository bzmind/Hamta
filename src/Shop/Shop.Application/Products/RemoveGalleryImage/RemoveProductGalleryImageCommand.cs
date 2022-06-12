using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.FileUtility;
using Shop.Domain.ProductAggregate.Repository;

namespace Shop.Application.Products.RemoveGalleryImage;

public record RemoveProductGalleryImageCommand(long ProductId, long GalleryImageId) : IBaseCommand;

public class RemoveProductGalleryImageCommandHandler : IBaseCommandHandler<RemoveProductGalleryImageCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IFileService _fileService;

    public RemoveProductGalleryImageCommandHandler(IProductRepository productRepository, IFileService fileService)
    {
        _productRepository = productRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(RemoveProductGalleryImageCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsTrackingAsync(request.ProductId);

        if (product == null)
            return OperationResult.NotFound();

        var oldImage = product.GalleryImages.FirstOrDefault(i => i.Id == request.GalleryImageId);

        if (oldImage == null)
            return OperationResult.NotFound();

        _fileService.DeleteFile(Directories.ProductGalleryImages, oldImage.Name);

        product.RemoveGalleryImage(request.GalleryImageId);

        await _productRepository.SaveAsync();
        return OperationResult.Success();
    }
}