using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.FileUtility;
using Common.Application.Utility.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shop.Domain.AvatarAggregate;
using Shop.Domain.AvatarAggregate.Repository;

namespace Shop.Application.Avatars.Create;

public record CreateAvatarCommand(IFormFile AvatarFile, Avatar.AvatarGender Gender) : IBaseCommand<long>;

public class CreateAvatarCommandHandler : IBaseCommandHandler<CreateAvatarCommand, long>
{
    private readonly IAvatarRepository _avatarRepository;
    private readonly IFileService _fileService;

    public CreateAvatarCommandHandler(IAvatarRepository avatarRepository, IFileService fileService)
    {
        _avatarRepository = avatarRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult<long>> Handle(CreateAvatarCommand request, CancellationToken cancellationToken)
    {
        var image = await _fileService.SaveFileAndGenerateName(request.AvatarFile, Directories.UserAvatars);
        var avatar = new Avatar(image, request.Gender);
        _avatarRepository.Add(avatar);
        await _avatarRepository.SaveAsync();
        return OperationResult<long>.Success(avatar.Id);
    }
}

public class CreateAvatarCommandValidator : AbstractValidator<CreateAvatarCommand>
{
    public CreateAvatarCommandValidator()
    {
        RuleFor(r => r.AvatarFile)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("عکس آواتار"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عکس آواتار"));

        RuleFor(r => r.Gender)
            .NotNull().WithMessage(ValidationMessages.GenderRequired)
            .IsInEnum().WithMessage(ValidationMessages.InvalidGender);
    }
}