using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.FileUtility;
using Common.Application.Utility.Validation;
using Common.Application.Utility.Validation.CustomAttributes;
using Common.Application.Utility.Validation.CustomFluentValidations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Repositories;

namespace Shop.Application.Entities.Banners.Edit;

public record EditBannerCommand(long Id, string Link, IFormFile? Image, Banner.BannerPosition Position) : IBaseCommand;

public class EditBannerCommandHandler : IBaseCommandHandler<EditBannerCommand>
{
    private readonly IBannerRepository _bannerRepository;
    private readonly IFileService _fileService;

    public EditBannerCommandHandler(IBannerRepository bannerRepository, IFileService fileService)
    {
        _bannerRepository = bannerRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await _bannerRepository.GetAsTrackingAsync(request.Id);
        if (banner == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("بنر"));

        var imageName = banner.Image;
        var oldImage = banner.Image;

        if (request.Image.IsImage())
            imageName = await _fileService
                .SaveFileAndGenerateName(request.Image, Directories.BannerImages);

        banner.Edit(request.Link, imageName, request.Position);

        await _bannerRepository.SaveAsync();
        if (request.Image.IsImage())
            _fileService.DeleteFile(Directories.BannerImages, oldImage);
        
        return OperationResult.Success();
    }
}

public class EditBannerCommandValidator : AbstractValidator<EditBannerCommand>
{
    public EditBannerCommandValidator()
    {
        RuleFor(r => r.Link)
            .NotEmpty().WithMessage(ValidationMessages.LinkRequired);

        RuleFor(r => r.Image)
            .JustImageFile();

        RuleFor(r => r.Position)
            .NotNull().WithMessage(ValidationMessages.ChooseBannerPosition)
            .IsInEnum().WithMessage(ValidationMessages.InvalidBannerPosition);
    }
}