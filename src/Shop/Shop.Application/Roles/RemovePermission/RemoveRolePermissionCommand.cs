using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.RoleAggregate;
using Shop.Domain.RoleAggregate.Repository;

namespace Shop.Application.Roles.RemovePermission;

public record RemoveRolePermissionCommand(long RoleId, List<RolePermission.Permissions> Permissions) : IBaseCommand;

public class RemoveRolePermissionCommandHandler : IBaseCommandHandler<RemoveRolePermissionCommand>
{
    private readonly IRoleRepository _roleRepository;

    public RemoveRolePermissionCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<OperationResult> Handle(RemoveRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetAsTrackingAsync(request.RoleId);

        if (role == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("نقش"));

        var permissions = new List<RolePermission>();
        request.Permissions.ForEach(p => permissions.Add(new RolePermission(p)));

        role.RemovePermissions(permissions);

        await _roleRepository.SaveAsync();
        return OperationResult.Success();
    }
}
public class RemoveRolePermissionCommandValidator : AbstractValidator<RemoveRolePermissionCommand>
{
    public RemoveRolePermissionCommandValidator()
    {
        RuleFor(r => r.Permissions)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("مجوز ها"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("مجوز ها"));
    }
}