using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.FileUtility;
using Common.Application.Utility.Validation;
using Shop.Domain.Entities.Repositories;

namespace Shop.Application.Entities.Sliders.Delete;

public record RemoveSliderCommand(long Id) : IBaseCommand;

public class RemoveSliderCommandHandler : IBaseCommandHandler<RemoveSliderCommand>
{
    private readonly ISliderRepository _sliderRepository;
    private readonly IFileService _fileService;

    public RemoveSliderCommandHandler(ISliderRepository sliderRepository, IFileService fileService)
    {
        _sliderRepository = sliderRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(RemoveSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.GetAsTrackingAsync(request.Id);
        if (slider == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("اسلایدر"));

        _sliderRepository.Delete(slider);
        _fileService.DeleteFile(Directories.SliderImages, slider.Image);

        await _sliderRepository.SaveAsync();
        return OperationResult.Success();
    }
}