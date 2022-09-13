using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.FileUtility;
using Common.Application.Utility.Validation;
using Common.Application.Utility.Validation.CustomFluentValidations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Repositories;

namespace Shop.Application.Entities.Banners.Create;

public record CreateBannerCommand(string Link, IFormFile Image, Banner.BannerPosition Position) : IBaseCommand<long>;

public class CreateBannerCommandHandler : IBaseCommandHandler<CreateBannerCommand, long>
{
    private readonly IBannerRepository _bannerRepository;
    private readonly IFileService _fileService;

    public CreateBannerCommandHandler(IBannerRepository bannerRepository, IFileService fileService)
    {
        _bannerRepository = bannerRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult<long>> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _fileService.SaveFileAndGenerateName(request.Image, Directories.BannerImages);
        var banner = new Banner(request.Link, imageName, request.Position);

        await _bannerRepository.AddAsync(banner);
        await _bannerRepository.SaveAsync();

        return OperationResult<long>.Success(banner.Id);
    }
}

public class CreateBannerCommandValidator : AbstractValidator<CreateBannerCommand>
{
    public CreateBannerCommandValidator()
    {
        RuleFor(r => r.Link)
            .NotEmpty().WithMessage(ValidationMessages.LinkRequired);

        RuleFor(r => r.Image)
            .NotEmpty().WithMessage(ValidationMessages.BannerImageRequired)
            .JustImageFile();

        RuleFor(r => r.Position)
            .NotNull().WithMessage(ValidationMessages.ChooseBannerPosition)
            .IsInEnum().WithMessage(ValidationMessages.InvalidBannerPosition);
    }
}