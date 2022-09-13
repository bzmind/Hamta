using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.FileUtility;
using Common.Application.Utility.Validation;
using Common.Application.Utility.Validation.CustomFluentValidations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Repositories;

namespace Shop.Application.Entities.Sliders.Create;

public record CreateSliderCommand(string Title, string Link, IFormFile Image) : IBaseCommand<long>;

public class CreateSliderCommandHandler : IBaseCommandHandler<CreateSliderCommand, long>
{
    private readonly IFileService _fileService;
    private readonly ISliderRepository _sliderRepository;

    public CreateSliderCommandHandler(IFileService fileService, ISliderRepository sliderRepository)
    {
        _fileService = fileService;
        _sliderRepository = sliderRepository;
    }

    public async Task<OperationResult<long>> Handle(CreateSliderCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _fileService
            .SaveFileAndGenerateName(request.Image, Directories.SliderImages);
        var slider = new Slider(request.Title, request.Link, imageName);

        await _sliderRepository.AddAsync(slider);
        await _sliderRepository.SaveAsync();

        return OperationResult<long>.Success(slider.Id);
    }
}

public class CreateSliderCommandValidator : AbstractValidator<CreateSliderCommand>
{
    public CreateSliderCommandValidator()
    {
        RuleFor(r => r.Title)
            .NotEmpty().WithMessage(ValidationMessages.TitleRequired);

        RuleFor(r => r.Link)
            .NotEmpty().WithMessage(ValidationMessages.LinkRequired);

        RuleFor(r => r.Image)
            .NotEmpty().WithMessage(ValidationMessages.BannerImageRequired)
            .JustImageFile();
    }
}