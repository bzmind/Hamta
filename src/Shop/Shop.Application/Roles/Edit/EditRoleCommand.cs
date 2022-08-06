using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.RoleAggregate;
using Shop.Domain.RoleAggregate.Repository;

namespace Shop.Application.Roles.Edit;

public class EditRoleCommand : IBaseCommand
{
    public long Id { get; init; }
    public string Title { get; init; }
    public List<RolePermission.Permissions> Permissions { get; init; }

    private EditRoleCommand() { }
}

public class EditRoleCommandHandler : IBaseCommandHandler<EditRoleCommand>
{
    private readonly IRoleRepository _roleRepository;

    public EditRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<OperationResult> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetAsTrackingAsync(request.Id);

        if (role == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("نقش"));

        var permissions = new List<RolePermission>();
        request.Permissions.ForEach(p => permissions.Add(new RolePermission(p)));

        role.Edit(request.Title, permissions);

        await _roleRepository.SaveAsync();
        return OperationResult.Success();
    }
}
public class EditRoleCommandValidator : AbstractValidator<EditRoleCommand>
{
    public EditRoleCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotNull().WithMessage(ValidationMessages.TitleRequired)
            .NotEmpty().WithMessage(ValidationMessages.TitleRequired)
            .MaximumLength(50).WithMessage(ValidationMessages.FieldCharactersMaxLength("عنوان", 50));

        RuleFor(r => r.Permissions)
            .NotNull().WithMessage(ValidationMessages.PermissionsRequired)
            .NotEmpty().WithMessage(ValidationMessages.PermissionsRequired);
    }
}