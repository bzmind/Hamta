using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.FileUtility;
using Common.Application.Utility.Validation;
using Common.Application.Utility.Validation.CustomFluentValidations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Entities.Repositories;

namespace Shop.Application.Entities.Sliders.Edit;

public record EditSliderCommand(long Id, string Title, string Link, IFormFile? Image) : IBaseCommand;

public class EditSliderCommandHandler : IBaseCommandHandler<EditSliderCommand>
{
    private readonly IFileService _fileService;
    private readonly ISliderRepository _sliderRepository;

    public EditSliderCommandHandler(IFileService fileService, ISliderRepository sliderRepository)
    {
        _fileService = fileService;
        _sliderRepository = sliderRepository;
    }
    public async Task<OperationResult> Handle(EditSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.GetAsTrackingAsync(request.Id);
        if (slider == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("اسلایدر"));

        var imageName = slider.Image;
        var oldImage = slider.Image;

        if (request.Image != null)
            imageName = await _fileService.SaveFileAndGenerateName(request.Image, Directories.SliderImages);

        slider.Edit(request.Title, request.Link, imageName);

        await _sliderRepository.SaveAsync();
        if (request.Image != null)
            _fileService.DeleteFile(Directories.SliderImages, oldImage);

        return OperationResult.Success();
    }
}

public class EditSliderCommandValidator : AbstractValidator<EditSliderCommand>
{
    public EditSliderCommandValidator()
    {
        RuleFor(r => r.Title)
            .NotEmpty().WithMessage(ValidationMessages.TitleRequired);

        RuleFor(r => r.Link)
            .NotEmpty().WithMessage(ValidationMessages.LinkRequired);

        RuleFor(r => r.Image)
            .JustImageFile();
    }
}