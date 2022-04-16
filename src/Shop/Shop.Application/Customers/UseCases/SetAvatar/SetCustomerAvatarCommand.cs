using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.FileUtility;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shop.Domain.CustomerAggregate;
using Shop.Domain.CustomerAggregate.Repository;

namespace Shop.Application.Customers.UseCases.SetAvatar;

public record SetCustomerAvatarCommand(long CustomerId, IFormFile AvatarName) : IBaseCommand;

public class SetCustomerAvatarCommandHandler : IBaseCommandHandler<SetCustomerAvatarCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IFileService _fileService;

    public SetCustomerAvatarCommandHandler(ICustomerRepository customerRepository, IFileService fileService)
    {
        _customerRepository = customerRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(SetCustomerAvatarCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAsTrackingAsync(request.CustomerId);

        if (customer == null)
            return OperationResult.NotFound();

        var oldAvatar = customer.AvatarName;
        if (oldAvatar != Customer.DefaultAvatarName)
            _fileService.DeleteFile(Directories.UserAvatars, oldAvatar);

        var newAvatar = await _fileService.SaveFileAndGenerateName(request.AvatarName, Directories.UserAvatars);
        customer.SetAvatar(newAvatar);
        
        await _customerRepository.SaveAsync();
        return OperationResult.Success();
    }
}

internal class SetCustomerAvatarCommandValidator : AbstractValidator<SetCustomerAvatarCommand>
{
    public SetCustomerAvatarCommandValidator()
    {
        RuleFor(a => a.AvatarName)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عکس"));
    }
}