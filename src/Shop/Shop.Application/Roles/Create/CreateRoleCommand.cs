using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.RoleAggregate;
using Shop.Domain.RoleAggregate.Repository;

namespace Shop.Application.Roles.Create;

public record CreateRoleCommand(string Title, List<RolePermission.Permissions> Permissions) : IBaseCommand<long>;

public class CreateRoleCommandHandler : IBaseCommandHandler<CreateRoleCommand, long>
{
    private readonly IRoleRepository _roleRepository;

    public CreateRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<OperationResult<long>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var permissions = new List<RolePermission>();
        request.Permissions.ForEach(p => permissions.Add(new RolePermission(p)));

        var role = new Role(request.Title, permissions);

        _roleRepository.Add(role);

        await _roleRepository.SaveAsync();
        return OperationResult<long>.Success(role.Id);
    }
}

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(r => r.Title)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("عنوان"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عنوان"));

        RuleFor(r => r.Permissions)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("مجوز ها"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("مجوز ها"));
    }
}