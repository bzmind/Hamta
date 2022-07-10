using Common.Api;
using Shop.Query.Roles._DTOs;
using System.Text.Json;
using Shop.Application.Roles.AddPermission;
using Shop.Application.Roles.Create;
using Shop.Application.Roles.RemovePermission;

namespace Shop.UI.Services.Roles;

public class RoleService : BaseService, IRoleService
{
    protected override string ApiEndpointName { get; set; } = "Role";

    public RoleService(HttpClient client, JsonSerializerOptions jsonOptions) : base(client, jsonOptions) { }

    public async Task<ApiResult> Create(CreateRoleCommand model)
    {
        return await PostAsJsonAsync("Create", model);
    }

    public async Task<ApiResult> AddPermissions(AddRolePermissionCommand model)
    {
        return await PutAsJsonAsync("AddPermissions", model);
    }

    public async Task<ApiResult> RemovePermissions(RemoveRolePermissionCommand model)
    {
        return await PutAsJsonAsync("RemovePermissions", model);
    }

    public async Task<ApiResult> Remove(long roleId)
    {
        return await DeleteAsync($"Remove/{roleId}");
    }

    public async Task<RoleDto> GetById(long roleId)
    {
        var result = await GetFromJsonAsync<RoleDto>($"GetById/{roleId}");
        return result.Data;
    }

    public async Task<List<RoleDto>> GetAll()
    {
        var result = await GetFromJsonAsync<List<RoleDto>>("api/role/GetAll");
        return result.Data;
    }
}