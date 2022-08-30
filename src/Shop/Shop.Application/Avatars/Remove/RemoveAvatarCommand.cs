using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.FileUtility;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.AvatarAggregate.Repository;

namespace Shop.Application.Avatars.Remove;

public record RemoveAvatarCommand(long AvatarId) : IBaseCommand;

public class RemoveAvatarCommandHandler : IBaseCommandHandler<RemoveAvatarCommand>
{
    private readonly IAvatarRepository _avatarRepository;
    private readonly IFileService _fileService;

    public RemoveAvatarCommandHandler(IAvatarRepository avatarRepository, IFileService fileService)
    {
        _avatarRepository = avatarRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(RemoveAvatarCommand request, CancellationToken cancellationToken)
    {
        var oldAvatar = await _avatarRepository.GetAsTrackingAsync(request.AvatarId);

        if (oldAvatar == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("آواتار"));

        _fileService.DeleteFile(Directories.UserAvatars, oldAvatar.Name);

        var canRemoveAvatar = _avatarRepository.RemoveAvatar(oldAvatar);
        if (!canRemoveAvatar)
            return OperationResult.Error(ValidationMessages.FieldCantBeRemoved("آواتار"));

        await _avatarRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class RemoveAvatarCommandValidator : AbstractValidator<RemoveAvatarCommand>
{
    public RemoveAvatarCommandValidator()
    {
        RuleFor(r => r.AvatarId)
            .NotEmpty().WithMessage(ValidationMessages.AvatarIdRequired);
    }
}