using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.FileUtility;
using Common.Application.Utility.Validation;
using Shop.Domain.Entities.Repositories;

namespace Shop.Application.Entities.Banners.Delete;

public record RemoveBannerCommand(long Id) : IBaseCommand;

public class RemoveBannerCommandHandler : IBaseCommandHandler<RemoveBannerCommand>
{
    private readonly IBannerRepository _bannerRepository;
    private readonly IFileService _fileService;
    public RemoveBannerCommandHandler(IBannerRepository bannerRepository, IFileService fileService)
    {
        _bannerRepository = bannerRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(RemoveBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await _bannerRepository.GetAsTrackingAsync(request.Id);
        if (banner == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("بنر"));

        _bannerRepository.Delete(banner);
        await _bannerRepository.SaveAsync();
        _fileService.DeleteFile(Directories.BannerImages, banner.Image);

        return OperationResult.Success();
    }
}