﻿using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.FileUtility;
using Common.Application.Utility.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shop.Domain.UserAggregate;
using Shop.Domain.UserAggregate.Repository;

namespace Shop.Application.Users.SetAvatar;

public record SetUserAvatarCommand(long UserId, IFormFile Avatar) : IBaseCommand;

public class SetUserAvatarCommandHandler : IBaseCommandHandler<SetUserAvatarCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IFileService _fileService;

    public SetUserAvatarCommandHandler(IUserRepository userRepository, IFileService fileService)
    {
        _userRepository = userRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(SetUserAvatarCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsTrackingAsync(request.UserId);

        if (user == null)
            return OperationResult.NotFound();

        var oldAvatar = user.AvatarName;
        if (oldAvatar != User.DefaultAvatarName)
            _fileService.DeleteFile(Directories.UserAvatars, oldAvatar);

        var newAvatar = await _fileService.SaveFileAndGenerateName(request.Avatar, Directories.UserAvatars);
        user.SetAvatar(newAvatar);

        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class SetUserAvatarCommandValidator : AbstractValidator<SetUserAvatarCommand>
{
    public SetUserAvatarCommandValidator()
    {
        RuleFor(a => a.Avatar)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("عکس"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عکس"));
    }
}