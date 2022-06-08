using Common.Api.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.RoleAggregate;
using Shop.Presentation.Facade.Roles;
using Shop.Presentation.Facade.Users;

namespace Common.Api.Attributes;

public class CheckPermissionAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private IUserFacade _userFacade;
    private IRoleFacade _roleFacade;
    private readonly RolePermission.Permissions _permission;

    public CheckPermissionAttribute(RolePermission.Permissions permission)
    {
        _permission = permission;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        _userFacade = context.HttpContext.RequestServices.GetRequiredService<IUserFacade>();
        _roleFacade = context.HttpContext.RequestServices.GetRequiredService<IRoleFacade>();

        if(IsAnonymous(context))
            return;

        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            if (await UserHasPermission(context) == false)
            {
                context.Result = new ForbidResult();
            }
        }
    }

    private bool IsAnonymous(AuthorizationFilterContext context)
    {
        var metaData = context.ActionDescriptor.EndpointMetadata.OfType<dynamic>().ToList();
        bool isAnonymous = false;
        foreach (var m in metaData)
        {
            try
            {
                isAnonymous = m.TypeId.Name == "AllowAnonymousAttribute";
                if (isAnonymous)
                    break;
            }
            catch (Exception e)
            {
                // ignored
            }
        }
        return isAnonymous;
    }

    private async Task<bool> UserHasPermission(AuthorizationFilterContext context)
    {
        var user = await _userFacade.GetById(context.HttpContext.User.GetUserId());

        if (user == null)
            return false;

        var roleIds = user.Roles.Select(r => r.RoleId).ToList();
        var roles = await _roleFacade.GetAll();
        var userRoles = roles.Where(r => roleIds.Contains(r.Id));

        return userRoles.Any(r => r.Permissions.Contains(_permission.ToString()));
    }
}