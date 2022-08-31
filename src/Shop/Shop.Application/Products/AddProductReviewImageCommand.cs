using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.FileUtility;
using Common.Application.Utility.Validation.CustomFluentValidations;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Shop.Application.Products;

public record AddProductReviewImageCommand(IFormFile Image) : IBaseCommand<string>;

public class AddProductReviewImageCommandHandler : IBaseCommandHandler<AddProductReviewImageCommand, string>
{
    private readonly IFileService _fileService;

    public AddProductReviewImageCommandHandler(IFileService fileService)
    {
        _fileService = fileService;
    }

    public async Task<OperationResult<string>> Handle(AddProductReviewImageCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _fileService.SaveFileAndGenerateName(request.Image, Directories.ProductReviewImages);
        var imageUrl = ServerPaths.GetProductReviewImagePath(imageName);
        return OperationResult<string>.Success(imageUrl);
    }
}

public class AddProductReviewImageCommandValidator : AbstractValidator<AddProductReviewImageCommand>
{
    public AddProductReviewImageCommandValidator()
    {
        RuleFor(r => r.Image).JustImageFile();
    }
}