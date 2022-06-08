using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.RoleAggregate;
using Shop.Domain.RoleAggregate.Repository;

namespace Shop.Application.Roles.AddPermission;

public record AddRolePermissionCommand(long RoleId, List<RolePermission.Permissions> Permissions) : IBaseCommand;

public class AddRolePermissionCommandHandler : IBaseCommandHandler<AddRolePermissionCommand>
{
    private readonly IRoleRepository _roleRepository;

    public AddRolePermissionCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<OperationResult> Handle(AddRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetAsTrackingAsync(request.RoleId);

        if (role == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("نقش"));

        var permissions = new List<RolePermission>();
        request.Permissions.ForEach(p => permissions.Add(new RolePermission(p)));

        role.AddPermissions(permissions);

        await _roleRepository.SaveAsync();
        return OperationResult.Success();
    }
}
public class AddRolePermissionCommandValidator : AbstractValidator<AddRolePermissionCommand>
{
    public AddRolePermissionCommandValidator()
    {
        RuleFor(r => r.Permissions)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("مجوز ها"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("مجوز ها"));
    }
}