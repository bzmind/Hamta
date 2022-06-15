using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using Shop.Domain.RoleAggregate.Repository;

namespace Shop.Application.Roles.Remove;

public record RemoveRoleCommand(long RoleId) : IBaseCommand;

public class RemoveRoleCommandHandler : IBaseCommandHandler<RemoveRoleCommand>
{
    private readonly IRoleRepository _roleRepository;

    public RemoveRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<OperationResult> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetAsync(request.RoleId);

        if (role == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("نقش"));

        _roleRepository.Delete(role);

        try
        {
            await _roleRepository.SaveAsync();
        }
        catch (Exception e)
        {
            return OperationResult.Error("امکان حذف این نقش وجود ندارد، زیرا نقش توسط کاربران دیگر در حال استفاده می‌باشد");
        }

        return OperationResult.Success();
    }
}